using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class FTasks : Entity
    {
        [BsonElement("subtasks")]
        private List<FTasks> Subtasks { get; set; } = new List<FTasks>();
        [BsonElement("employees")]
        private List<Employee> EmployeesAssigned { get; set; } = new List<Employee>();

        public FTasks(string name) : base(name) { }
        public FTasks(string name, DateTime created) : base(name, created) { }
    }
}