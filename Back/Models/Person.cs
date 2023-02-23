using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Person : Entity
    {
        [BsonElement("lastName")]
        private string LastName { get; set; }
        [BsonElement("contact")]
        private Contact Contacts { get; set; }
        [BsonElement("job")]
        private Job? Charges { get; set; }

        public Person(string name, string last) : base(name)
        {
            LastName = last;
            Contacts = new Contact();
        }
    }
}