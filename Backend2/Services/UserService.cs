using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository
{
    public class UserService
        : BaseRepositoryService<User, LoginSuccessDto, NewUserDto>,
            IRepositoryService<User, LoginSuccessDto, NewUserDto>
    {
        public Dictionary<string, Action<User, dynamic>>
            Properties { get; } = new()
            {
                { "Name", typeof(User).GetProperty("Name")!.SetValue },
                { "Password", typeof(User).GetProperty("Password")!.SetValue },
                { "Avatar", typeof(User).GetProperty("Avatar")!.SetValue },
                { "Active", typeof(User).GetProperty("Active")!.SetValue },
                { "Email", typeof(User).GetProperty("Email")!.SetValue },
                { "Roles", typeof(User).GetProperty("Roles")!.SetValue },
                { "Credentials", typeof(User)
                    .GetProperty("Credentials")!.SetValue }
            };
        public Dictionary<string, Dictionary<string, Action<object?, object?>>>
            Embedded { get; } = new()
            {
                { "Credentials", new Dictionary<string, Action<object?, object?>>()
                    {
                        { "Claims",  typeof(Credentials).GetProperty("Claims")!.SetValue },
                        { "Identity",  typeof(Credentials).GetProperty("Identity")!.SetValue }
                    }
                }
            };

        public UserService(MongoSettings container)
            : base(container, "cbs", "users") { }
    }
}