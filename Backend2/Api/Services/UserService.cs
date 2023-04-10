using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;
using MongoDB.Driver;

namespace Fictichos.Constructora.Repository;

public class UserService
    : BaseService<User, NewUserDto, UpdatedUserDto>
{
    private const string MAINCOLLECTION = "users";
    public UserService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }
    
    public bool NameIsUnique(string name)
    {
        FilterDefinition<User> filter = Builders<User>
            .Filter
            .Eq(x => x.Name, name);
        if (GetOneBy(filter) is not null)
            return false;
        return true;
    }

    public async Task<bool> UserIsValid(string id)
    {
        User? usr = await _mainCollection
            .GetOneByFilterAsync(Filter.ById<User>(id));
        if (usr is null) return false;
        
        return usr.Active;
    }
}