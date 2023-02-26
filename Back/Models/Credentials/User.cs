using Isopoh.Cryptography.Argon2;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
{
  [BsonIgnoreExtraElements]
    public class User : Entity
    {
        [BsonElement("username")]
        public new string Name { get; init; }
        [BsonElement("password")]
        private string Password { get; set; }
        [BsonElement("avatar")]
        private byte[]? Avatar { get; set; }
        [BsonElement("created")]
        private DateTime Created { get; } = DateTime.Now;
        [BsonElement("active")]
        private bool Active { get; set; } = false;
        [BsonElement("email")]
        private List<string> Email { get; set; } = new List<string>();
        // private List<string> MAC { get; set; } = new List<string>();
        [BsonElement("publicAuthKey")]
        private string PublicAuthKey { get; set; } = 
                Argon2.Hash((new Random().NextDouble() * long.MaxValue)
                    .ToString());

        public User(NewUserDTO newUser) : base(newUser.Username)
        {
            Name = newUser.Username;
            Password = newUser.Password;
            Email.Add(newUser.Email);
        }

        public void UpdatePassword(string pwd)
        {
            Password = pwd;
        }

        public bool ValidatePassword(string pwd)
        {
            return Argon2.Verify(Password, pwd);
        }

        public UserDTO AsDTO()
        {
            return new UserDTO
            {
                Username = this.Name,
                Password = this.Password,
                Active = this.Active,
                Email = this.Email
            };
        }

    /**
    /// <summary>
    /// Finds the MAC address of the NIC with maximum speed.
    /// </summary>
    /// <returns>The MAC address.</returns>
    private static class MACCatcher
    {
        public static string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }
    }
    */
  }
}