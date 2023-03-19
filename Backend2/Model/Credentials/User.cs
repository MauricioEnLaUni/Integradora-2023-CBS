using Isopoh.Cryptography.Argon2;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class User : AbstractEntity<User, LoginSuccessDto, NewUserDto>
    {
        public string Name { get; init; } = string.Empty;
        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => _password = Argon2.Hash(value);
        }
        public byte[]? Avatar { get; private set; }
        public bool Active { get; private set; } = false;
        public List<string> Email { get; private set; } = new();
        public List<string> Roles { get; private set; } = new();
        // MAC?
        // AUTH
        // ROLES = EMPLEADO, ADMIN DE PROYECTO, ADMIN GENERAL
        public List<ICredentials> Credentials { get; private set; } = new();

        public User() { }
        public User(NewUserDto usr)
        {
            Name = usr.Name;
            Password = usr.Password;
            Email.Add(usr.Email);
        }

        public bool ValidatePassword(string pwd)
        {
            return Argon2.Verify(Password, pwd);
        }

        public override LoginSuccessDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name
            };
        }

        public override User Instantiate(string dto)
        {
            try
            {
                return new User(JsonConvert
                    .DeserializeObject<NewUserDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public override User Instantiate(NewUserDto dto)
        {
            return new(dto);
        }
    }
}