using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Fictichos.Constructora.Middleware;

public static class TokenValidator
{
    public static IEnumerable<Claim> GetClaims(string data)
    {
        JwtSecurityTokenHandler handler = new();
        var jsonToken = handler.ReadJwtToken(data);

        return jsonToken.Claims;
    }
}