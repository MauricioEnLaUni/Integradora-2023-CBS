using System.Drawing;
using System.Net.NetworkInformation;
using Isopoh.Cryptography.Argon2;

namespace Fictichos.Credentials
{
    public class User
    {
        private string Id { get; }
        private string Username { get; init; }
        private string Password { get; set; }
        private Image? Avatar { get; set; }
        private DateTime Created { get; }= DateTime.Now;
        private bool Active { get; set; }
        private List<string> Email { get; set; } = new List<string>();
        private List<string> MAC { get; set; } = new List<string>();
        private string PublicAuthKey { get; set; }

        public string CreatedId()
        {
            return "0";
        }

        public User(string usr, string pwd, string email)
        {
            Id = CreatedId();
            Username = usr;
            Password = pwd;
            MAC.Add(MACCatcher.GetMacAddress());
            Email.Add(email);
            PublicAuthKey = Argon2.Hash((new Random().NextDouble() * long.MaxValue).ToString());
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