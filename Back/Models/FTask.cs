using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
{
    public class FTasks : Entity
    {
        [BsonElement("subtasks")]
        public List<FTasks> Subtasks { get; set; } = new List<FTasks>();
        [BsonElement("employees")]
        public List<Employee> EmployeesAssigned { get; set; } = new List<Employee>();
        [BsonElement("material")]
        public List<Material> Material { get; set; } = new List<Material>();
        [BsonElement("address")]
        public Address? Address { get; set; }

        public FTasks(string name) : base(name) { }
        public FTasks(string name, DateTime created) : base(name, created) { }

        public FTasks(NewFTaskDTO newTask) : base(newTask.Name, newTask.Closed)
        {
            EmployeesAssigned = newTask.Assignees;
            if (newTask.Address is not null) Address = newTask.Address;
        }

        public FTaskInfoDTO AsDTO()
        {
            return new FTaskInfoDTO()
            {
                Name = this.Name,
                Created = this.CreatedAt,
                Closed = this.Closed,
                Subtasks = this.Subtasks,
                Assignees = this.EmployeesAssigned,
                Material = this.Material,
                Address = this.Address
            };
        }
    }
}