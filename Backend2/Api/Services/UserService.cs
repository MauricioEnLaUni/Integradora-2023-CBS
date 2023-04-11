using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;
using System.Security.Claims;

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

    public void GrantEmail(string id, string email)
    {
        Claim toAdd = new("owner", email);
        UpdateDefinition<User> update = Builders<User>
            .Update
            .AddToSet(x => x.Credentials, toAdd);
        _mainCollection.UpdateOne(Filter.ById<User>(id), update);
    }

    public User? AuthRoles(
        string sub, List<string>? allRoles, List<string>? anyRoles)
    {
        User? usr = GetOneBy(Filter.ById<User>(sub));
        if (usr is null) return null;
        if (!usr.Active) return null;

        if (usr.IsAdmin()) return usr;
        List<string> claims = usr.Credentials.Where(x => x.Type == "role")
            .Select(x => x.Value)
            .ToList();
        if (ValidateAllRoles(claims, allRoles) is false) return null;
        if (ValidateAnyRole(claims, anyRoles)) return usr;
        return null;
    }

    public bool ValidateAllRoles(List<string> claims, List<string>? roles)
    {
        if (roles is null || roles.Count == 0) return true;
        return roles.All(c => claims.Contains(c));
    }

    public bool ValidateAnyRole(List<string> claims, List<string>? roles)
    {
        if (roles is null || roles.Count == 0) return true;
        return roles.Any(c => claims.Contains(c));
    }
}