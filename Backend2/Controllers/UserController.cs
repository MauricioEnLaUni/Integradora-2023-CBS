using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Controllers
{
    public class UserController
        : FApiControllerBase<User, NewUserDto, UpdatedUserDto, UserService>
    {
        protected override UserService Repo { get; init; }

        public UserController(UserService _repo)
        {
            Repo = _repo;
        }
    }
}