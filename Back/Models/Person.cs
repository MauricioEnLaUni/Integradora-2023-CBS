using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Person : Entity
    {
        [BsonElement("lastName")]
        public string LastName { get; private set; }
        [BsonElement("contact")]
        private Contact Contacts { get; set; } = new Contact();
        [BsonElement("job")]
        public List<Job> Charges { get; private set; } = new List<Job>();

        public Person(string name, string last) : base(name)
        {
            LastName = last;
        }
    }
}