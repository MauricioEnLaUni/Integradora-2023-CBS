using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Contact
    {
        [BsonElement("addresses")]
        private List<Address> Addresses { get; set; } = new List<Address>();
        [BsonElement("phones")]
        private List<string> Phones { get; set; } = new List<string>();
        [BsonElement("emails")]
        private List<string> Emails { get; set; } = new List<string>();

        public Contact() { }
    }
}