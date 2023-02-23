using System.Drawing;
using System.Net.NetworkInformation;
using Fictichos.Constructora.Utils.Generics;
using Isopoh.Cryptography.Argon2;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Credentials
{
  [BsonIgnoreExtraElements]
    public class User : ILinqSearchable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string Id { get; }
        [BsonElement("credentials")]
        public Dictionary<string, string>? Credentials { get; private set; }
        [BsonElement("avatar")]
        private Image? Avatar { get; set; }
        [BsonElement("created")]
        private DateTime Created { get; } = DateTime.Now;
        [BsonElement("active")]
        private bool Active { get; set; }
        [BsonElement("email")]
        private List<string> Email { get; set; } = new List<string>();
        private List<string> MAC { get; set; } = new List<string>();
        [BsonElement("publicAuthKey")]
        private string PublicAuthKey { get; set; }

        public string CreatedId()
        {
            return "0";
        }

        public User(string usr, string pwd, string email)
        {
            Id = CreatedId();
            Credentials = new Dictionary<string, string>()
            {
                { usr, pwd }
            };
            MAC.Add(MACCatcher.GetMacAddress());
            Email.Add(email);
            PublicAuthKey = Argon2.Hash((new Random().NextDouble() * long.MaxValue).ToString());
        }

        public void UpdatePassword(string usr, string pwd)
        {
            Credentials[usr] = pwd;
        }

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
    }
}