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
        public List<Claim> Ownership { get; set; } = new();
        public List<Claim> Membership { get; set; } = new();

        Credentials(User user, Person employee)
        {
            Claims.Add(new Claim(ClaimTypes.Name, user.Name));
            Claims.Add(new Claim("employee", employee.Id));
            Claims.Add(new Claim("internalKey", employee.InternalKey));
            SetRole(employee.Charges);
            

            Identity = new ClaimsIdentity(Claims, Constants.COOKIENAME);
        }

        internal void Subscribe(List<string> data)
        {
            data.ForEach(e => {
                Membership.Add(new Claim("member", e));
            });
        }
        
        internal void Claim()
        {

        }

        internal void SetRole(List<Job> data)
        {
            Job? current = data.Where(x => x.Active).SingleOrDefault();
            if (current is null) return;

            Claims.Add(new Claim("role", current.Role));
        }
    }

    public class AdminCredentials : ICredentials
    {

    }

    public class ManagerCredentials : ICredentials
    {

    }
}