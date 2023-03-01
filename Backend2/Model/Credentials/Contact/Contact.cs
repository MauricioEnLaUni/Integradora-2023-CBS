using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
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

        public Contact(NewContactDto data)
        {
            Addresses = data.Addresses.Select(a => new Address(a)).ToList();
            Phones = data.Phones;
            Emails = data.Emails;
        }
    }
}