using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
{
    public class Contact
    {
        [BsonElement("addresses")]
        public List<Address> Addresses { get; set; } = new List<Address>();
        [BsonElement("phones")]
        public List<string> Phones { get; set; } = new List<string>();
        [BsonElement("emails")]
        public List<string> Emails { get; set; } = new List<string>();

        public Contact() { }

        public Contact(NewContactDTO data)
        {
            Addresses = data.Addresses.Select(a => new Address(a)).ToList();
            Phones = data.Phones;
            Emails = data.Emails;
        }

        public ContactInfoDTO AsDTO()
        {
            return new ContactInfoDTO
            {
                Addresses = this.Addresses.Select(a => a.AsDTO()).ToList(),
                Phones = this.Phones,
                Emails = this.Emails
            };
        }
    }
}