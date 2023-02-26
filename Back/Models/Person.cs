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
        [BsonElement("isEmployed")]
        public Employee? Employed { get; private set; } 

        public Person(string name, string last, Employee? job) : base(name)
        {
            LastName = last;
            if (job != null) Employed = job;
        }
    }
}