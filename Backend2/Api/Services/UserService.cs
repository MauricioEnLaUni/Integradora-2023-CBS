using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository
{
    public class UserService
        : BaseRepositoryService<User, NewUserDto, UpdatedUserDto, LoginSuccessDto>
    {
        public UserService(MongoSettings container)
            : base(container, "cbs", "users") { }
    }
}