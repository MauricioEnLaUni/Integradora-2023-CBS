using Isopoh.Cryptography.Argon2;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

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

        public List<(int, int)> SetPassword(string data)
        {
            if (data.Length < 8) return new List<(int, int)>{ (1, 1) };
            if (data.Length > 64) return new List<(int, int)> { (1, 2) };
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$";
            if (!Regex.IsMatch(data, pattern)) return new
                List<(int, int)>{ (1, 3) };
            Password = Argon2.Hash(data);
            return new List<(int, int)>{ (1, 0) };
        }

        public List<(int, int)> SetAvatar(byte[] data)
        {
            Avatar = data;
            return new List<(int, int)>{ (2, 0) };
        }

        public List<(int, int)> KillOwnAccount()
        {
            Active = false;
            Closed = DateTime.Now;
            return new List<(int, int)>{ (0, 0) };
        }

        public List<(int, int)> UserSelfUpdate(UserSelfUpdateDto data)
        {
            List<(int, int)> results = new();

            if (data.killAccount is not null)
            {
                results.AddRange(KillOwnAccount());
            }
            if (data.password is not null)
            {
                results.AddRange(SetPassword(data.password));
            }
            if (data.avatar is not null)
            {
                results.AddRange(SetAvatar(data.avatar));
            }
            data.email?.ForEach(Email.UpdateWithIndex);

            return results;
        }

        public void Update(UpdatedUserDto data)
        {
            
        }

        public void Update(UserAdminUpdateDto data)
        {
            UserSelfUpdate(data.basicFields);
            data.roles?.ForEach(Roles.UpdateWithIndex);
        }
    }
}