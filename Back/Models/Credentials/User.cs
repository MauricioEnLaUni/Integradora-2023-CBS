using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Isopoh.Cryptography.Argon2;
using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
{
  [BsonIgnoreExtraElements]
    public class User : Entity
    {
        [BsonElement("username")]
        public new string Name { get; init; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("avatar")]
        public byte[]? Avatar { get; set; }
        [BsonElement("created")]
        private DateTime Created { get; } = DateTime.Now;
        [BsonElement("active")]
        public bool Active { get; set; } = false;
        [BsonElement("email")]
        public List<string> Email { get; set; } = new List<string>();
        [BsonElement("publicAuthKey")]
        private string PublicAuthKey { get; set; } = 
                Argon2.Hash((new Random().NextDouble() * long.MaxValue)
                    .ToString());

        public User(NewUserDTO newUser) : base(newUser.Username)
        {
            Name = newUser.Username;
            Password = Argon2.Hash(newUser.Password);
            Email.Add(newUser.Email);
        }

        public bool ValidatePassword(string pwd)
        {
            return Argon2.Verify(Password, pwd);
        }

        public UserLoginDTO AsLoginDTO()
        {
            return new UserLoginDTO
            {
                Username = this.Name,
                Password = this.Password,
                Active = this.Active
            };
        }

        public UserInfoDTO AsInfoDTO()
        {
            return new UserInfoDTO
            {
                Username = this.Name,
                Email = this.Email,
                Avatar = this.Avatar,
                Created = this.Created
            };
        }
  }
}