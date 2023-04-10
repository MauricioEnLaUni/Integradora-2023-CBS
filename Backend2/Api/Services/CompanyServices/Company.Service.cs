using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

public class CompanyService
    : BaseService<Company, NewCompanyDto, UpdatedCompanyDto>
{
    private const string MAINCOLLECTION = "companies";

    public CompanyService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    public bool NameIsUnique(string name)
    {
        FilterDefinition<Company> filter = Builders<Company>
            .Filter
            .Eq(x => x.Name, name);
        if (GetOneBy(filter) is not null)
            return false;
        return true;
    }
}
