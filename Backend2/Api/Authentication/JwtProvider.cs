using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Abstraction;

namespace Fictichos.Constructora.Auth
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string Generate(LoginResponseDto usr)
        {
            List<Claim> claims = new()
            {
                new(JwtRegisteredClaimNames.Sub, usr.id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, usr.name.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.Key)),
                algorithm: SecurityAlgorithms.HmacSha256
            );
            
            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: null,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}