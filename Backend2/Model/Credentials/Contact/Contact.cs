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
            if (data.Addresses is not null)
            {
                Addresses.Add(new(data.Addresses));
            }
            if (data.Phones is not null) Phones.Add(data.Phones);
            if (data.Emails is not null) Emails.Add(data.Emails);
        }
    }
}