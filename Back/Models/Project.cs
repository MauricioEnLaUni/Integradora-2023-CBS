using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    [BsonIgnoreExtraElements]
    public class Project: Entity
    {
        [BsonElement("client")]
        private Person Client { get; set; }
        [BsonElement("admin")]
        private Person Admin { get; set; }
        [BsonElement("tasks")]
        private IEnumerable<ProjectTask> Tasks { get; set; }
        [BsonElement("deadline")]
        private DateTime Deadline { get; set; }
        [BsonElement("payment")]
        private Account Payment { get; set; }
        
        public Project(string name, Person client) : base(name)
        {
            
        }
    }

    [BsonIgnoreExtraElements]
    public class Person: Entity
    {
        public Person(string name) : base(name)
        {

        }
    }

    [BsonIgnoreExtraElements]
    public class ProjectTask: Entity
    {
        public ProjectTask(string name) : base(name)
        {

        }
    }

    [BsonIgnoreExtraElements]
    public class Account: Entity
    {
        public Account(string name) : base(name)
        {

        }
    }

    [BsonIgnoreExtraElements]
    public class User: Person
    {
        public User(string name) : base(name)
        {

        }
    }
}