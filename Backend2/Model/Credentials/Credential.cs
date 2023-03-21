using System.Security.Claims;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class ICredentials
    {
    }

    public class Credentials : ICredentials
    {
        public List<Claim> Claims { get; set; } = new();
        public ClaimsIdentity Identity { get; set; }

        Credentials(User data)
        {
            Claims.Add(new Claim(ClaimTypes.Name, data.Name));
            Claims.Add(new Claim(ClaimTypes.Email, data.Email[0]));

            Identity = new ClaimsIdentity(Claims, Constants.COOKIENAME);
        }

        public void Update(UpdatedCredentialsDto data)
        {
            Identity = data.Identity ?? Identity;
            data.Claims?.ForEach(e => {
                    
                });
        }
    }

    public class AdminCredentials : ICredentials
    {

    }

    public class ManagerCredentials : ICredentials
    {

    }
}