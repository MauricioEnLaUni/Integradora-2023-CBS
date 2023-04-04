using System.Security.Claims;

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

        Credentials(User data, Employee employee)
        {
            Claims.Add(new Claim(ClaimTypes.Name, data.Name));
            ClaimEmails(data);
            Claims.Add(new Claim("employee", employee.Id));
            ClaimProjects(employee.Oversees);
            ClaimMaterial(employee);
            

            Identity = new ClaimsIdentity(Claims, Constants.COOKIENAME);
        }

        internal void ClaimProjects(List<string> data)
        {
            data.ForEach(e => {
                Claims.Add(new Claim("project-owner", e));
            });
        }

        internal void ClaimMaterial(Employee data)
        {
            List<string>? mats = data.Charges.MaxBy(x => x.CreatedAt)?.Material;
            mats?.ForEach(e => {
                Claims.Add(new Claim("material-owner", e));
            });
        }

        internal void ClaimMembership(Employee data)
        {
            data.Assignments.ForEach(e => {
                Claims.Add(new Claim("project-member", e));
            });
            if (data.Current() is not null)
            {
                Claims.Add(new Claim("area-member", data.Current()!.Id));
            }
        }

        internal void ClaimEmails(User data)
        {
            data.Email.ForEach(e => {
                Claims.Add(new Claim(ClaimTypes.Email, e));
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