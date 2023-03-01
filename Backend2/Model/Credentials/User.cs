using MongoDB.Bson.Serialization.Attributes;
using Isopoh.Cryptography.Argon2;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;

namespace Fitichos.Constructora.Model
{
    public class User : Entity
    {
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("avatar")]
        public byte[]? Avatar { get; set; }
        [BsonElement("active")]
        public bool Active { get; set; } = false;
        [BsonElement("email")]
        public List<string> Email { get; set; } = new List<string>();
        
        public User(NewUserDto usr) : base(usr.Name, null)
        {
            Password = usr.Password;
            Email.Add(usr.Email);
        }

        public bool ValidatePassword(string pwd)
        {
            return Argon2.Verify(Password, pwd);
        }

        public void Change(UserChangesDto changes)
        {
            if (changes.Avatar is not null) Avatar = changes.Avatar;
            if (changes.Email is not null) Email = changes.Email;
            if (changes.Password is not null) Password = changes.Password;
        }
    }
}