using MongoDB.Bson.Serialization.Attributes;
using Isopoh.Cryptography.Argon2;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class User : Entity, IQueryMask<User, LoginSuccessDto, UpdatedUserDto>
    {
        private string _password = string.Empty;
        [BsonElement("password")]
        public string Password
        {
            get => _password;
            set => _password = Argon2.Hash(value);
        }
        [BsonElement("avatar")]
        public byte[]? Avatar { get; private set; }
        [BsonElement("active")]
        public bool Active { get; private set; } = false;
        [BsonElement("email")]
        public List<string> Email { get; private set; } = new();
        // [BsonElement("roles")]
        // public List<string> Roles { get; private set; } = new();
        // MAC?
        // AUTH
        // ROLES = EMPLEADO, ADMIN DE PROYECTO, ADMIN GENERAL

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

        public void Update(UpdatedUserDto changes)
        {
            if (changes.Avatar is not null) Avatar = changes.Avatar;
            if (changes.Email is not null) Email = changes.Email;
            if (changes.Password is not null) Password = changes.Password;
        }

        public void SetActive()
        {
            Active = !Active;
        }

        public LoginSuccessDto ToDto()
        {
            return new()
            {
                Name = Name,
                CreatedAt = CreatedAt,
                Email = Email,
                Avatar = Avatar
            };
        }

        public string SerializeDto()
        {
            LoginSuccessDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public User FakeConstructor(string dto)
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
    }
}