using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository;

public class CompanyService
    : BaseService<Company, NewCompanyDto, UpdatedCompanyDto>
{
    private const string MAINCOLLECTION = "companies";

    public CompanyService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    public bool NameIsUnique(string? name)
    {
        if (name is null) return true;
        FilterDefinition<Company> filter = Builders<Company>
            .Filter
            .Eq(x => x.Name, name);
        if (GetOneBy(filter) is not null)
            return false;
        return true;
    }

    public async Task<bool> RemovePerson(string id, ExternalPerson employee)
    {
        Company? parent = await _mainCollection
            .GetOneByFilterAsync(Filter.ById<Company>(id));
        if (parent is null) return false;
        
        UpdateDefinition<Company> update = Builders<Company>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Pull(x => x.Members, employee);
        await _mainCollection.UpdateOneAsync(Filter.ById<Company>(id), update);
        return true;
    }

    public void AddMember(NewExPersonDto data)
    {
        ExternalPerson item = new ExternalPerson().Instantiate(data);
        UpdateDefinition<Company> update = Builders<Company>
            .Update
            .AddToSet(x => x.Members, item);
        _mainCollection.UpdateOne(Filter.ById<Company>(data.Owner), update);
    }

    public int BrowserUpdate(BrowserUpdateCompanyDto data)
    {
        if (!NameIsUnique(data.Name)) return 409;
        UpdateDefinition<Company> update = Builders<Company>.Update.Set(x => x.ModifiedAt, DateTime.Now);
        if (data.Name is null && data.Activity is null && data.Relation is null)
            return 400;
        update = data.Name is not null ? update.Set(x => x.Name, data.Name) : update;
        update = data.Activity is not null ? update.Set(x => x.Activity, data.Activity) : update;
        update = data.Relation is not null ? update.Set(x => x.Relation, data.Relation) : update;

        _mainCollection.UpdateOne(Filter.ById<Company>(data.Id), update);
        return 200;
    }
}
