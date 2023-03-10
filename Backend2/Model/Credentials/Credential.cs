using System.Security.Claims;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class ICredentials
    {
    }

    public class EmployeeCredentials : ICredentials
    {
        private List<Claim> Claims = new();
        private ClaimsIdentity Identity;

        EmployeeCredentials(User data)
        {
            Claims.Add(new Claim(ClaimTypes.Name, data.Name));
            Claims.Add(new Claim(ClaimTypes.Email, data.Email[0]));

            Identity = new ClaimsIdentity(Claims, Constants.COOKIENAME);
        }
    }

    public class AdminCredentials : ICredentials
    {

    }

    public class ManagerCredentials : ICredentials
    {

    }
}