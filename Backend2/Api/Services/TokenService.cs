using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Middleware;

namespace Fictichos.Constructora.Auth;

public class TokenService
    : BaseService<TokenContainer, NewTokenDto, UpdatedTokenDto>
{
    private const string MAINCOLLECTION = "activeTokens";
    private List<string> Tokens { get; set; } = new();

    public TokenService(MongoSettings container)
        : base(container, MAINCOLLECTION)
    {
        
        IndexKeysDefinition<TokenContainer> indexKeysDefinition =
            Builders<TokenContainer>.IndexKeys.Ascending(x => x.Expires);
        CreateIndexOptions<TokenContainer> indexOptions =
            new()
            { ExpireAfter = TimeSpan.Zero };
        _mainCollection.Indexes.CreateOne(new CreateIndexModel<TokenContainer>(indexKeysDefinition, indexOptions));

    }

    public TokenContainer AddToken(string token)
    {
        NewTokenDto data = new() { Token = token };
        TokenContainer output = new TokenContainer().Instantiate(data);

        _mainCollection.InsertOne(output);
        return output;
    }

    public JwtSecurityToken ParseToken(string token)
    {
        JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
        JwtSecurityToken output = jwt.ReadJwtToken(token);
        return(output);
    }

    public bool? AuthorizeAll(
        List<Claim> claims, string cookie, Dictionary<string, string> policy)
    {
        if (claims is null) return null;
        JwtSecurityToken jwt = ParseToken(cookie);
        
        if (jwt.ValidFrom > jwt.ValidTo) return null;
        if (jwt.ValidTo < DateTime.UtcNow) return null;
        
        if (jwt.Payload is null || claims is null) return false;

        foreach (KeyValuePair<string, string> e in policy)
        {
            var claim = claims.Where(c => c.Type == e.Key)
                .Select(c => c.Value)
                .ToList();
            if (!claim.Contains(e.Value))
            {
                return false;
            }
        }

        return true;
    }

    public bool? AuthorizeAny(
        List<Claim> claims, string cookie, Dictionary<string, string> policy)
    {
        if (claims is null) return null;
        JwtSecurityToken jwt = ParseToken(cookie);
        
        if (jwt.ValidFrom > jwt.ValidTo) return null;
        if (jwt.ValidTo < DateTime.UtcNow) return null;
        
        if (jwt.Payload is null || claims is null) return false;

        foreach (KeyValuePair<string, string> e in policy)
        {
            var claim = claims.Where(c => c.Type == e.Key)
                .Select(c => c.Value)
                .ToList();
            if (claim.Contains(e.Value))
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerable<Claim> GetClaimsFromHeader(string header)
    {
        header = header.Replace("Bearer ", "");
        return TokenValidator.GetClaims(header);
    }
}