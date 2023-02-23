using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Project : Entity
    {
        [BsonElement("account")]
        private Account PayHistory { get; set; }
        [BsonElement("tasks")]
        private List<FTasks> Tasks { get; } = new List<FTasks>();

        public Project(string name, string account) : base(name)
        {
            PayHistory = new Account(account);
        }
    }
}