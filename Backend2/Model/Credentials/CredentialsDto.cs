using System.Security.Claims;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto
{
    public record UpdatedCredentialsDto
    {
        public List<Claim>? Claims { get; set; }
        public ClaimsIdentity? Identity { get; set; }
    }
}