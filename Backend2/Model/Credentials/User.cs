using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;
using Isopoh.Cryptography.Argon2;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class User : Entity
    {
        [BsonElement("password")]
        public string Password { get; private set; }
        [BsonElement("avatar")]
        public byte[]? Avatar { get; private set; }
        [BsonElement("active")]
        public bool Active { get; private set; } = false;
        [BsonElement("email")]
        public List<string> Email { get; private set; } = new();
        [BsonElement("roles")]
        public List<string> Roles { get; private set; } = new();
        // MAC?
        // AUTH
        // ROLES = EMPLEADO, ADMIN DE PROYECTO, ADMIN GENERAL
        
        public User(NewUserDto usr) : base(usr.Name, null)
        {
            Password = Argon2.Hash(usr.Password);
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

        public void SetActive()
        {
            Active = !Active;
        }

        public string AsDto()
        {
            LoginSuccessDto data = new(this);
            return JsonConvert.SerializeObject(data);
        }
    }
}