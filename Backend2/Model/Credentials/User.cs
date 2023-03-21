using Isopoh.Cryptography.Argon2;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class User
        : BaseEntity, IQueryMask<User, NewUserDto, UpdatedUserDto, LoginSuccessDto>
    {
        public string Name { get; init; } = string.Empty;
        public DateTime? Closed { get; set; }
        public string Password { get; set; } = string.Empty;
        public byte[]? Avatar { get; private set; }
        public bool Active { get; private set; } = false;
        public List<string> Email { get; set; } = new();
        public List<string> Roles { get; private set; } = new();
        // MAC?
        // AUTH
        // ROLES = EMPLEADO, ADMIN DE PROYECTO, ADMIN GENERAL
        public List<ICredentials> Credentials { get; private set; } = new();

        public User() { }
        public User(NewUserDto data)
        {
            Name = data.Name;
            Password = Argon2.Hash(data.Password);
            Email.Add(data.Email);
        }

        public bool ValidatePassword(string pwd)
        {
            return Argon2.Verify(Password, pwd);
        }

        public LoginSuccessDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                CreatedAt = CreatedAt,
                Roles = Roles,
                Email = Email,
                Avatar = Avatar
            };
        }

        public string Serialize()
        {
            LoginSuccessDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public User Instantiate(NewUserDto dto)
        {
            return new(dto);
        }

        public void Update(UpdatedUserDto data)
        {
            Avatar = data.Avatar ?? Avatar;
            Password = data.Password is null ? Password : Argon2.Hash(data.Password);
            Active = data.Active ?? Active;
            Closed = data.Closed ?? Closed;
            
            data.Email?.ForEach(Email.UpdateWithIndex);
            data.Roles?.ForEach(Roles.UpdateWithIndex);
        }
    }
}