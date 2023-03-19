using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository
{
    public class UserService
        : BaseRepositoryService<User, LoginSuccessDto, NewUserDto>,
            IRepositoryService<User, LoginSuccessDto, NewUserDto>
    {
        public override Dictionary<string, Action<User, dynamic>>
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

        public UserService(MongoSettings container)
            : base(container, "cbs", "users") { }
    }
}