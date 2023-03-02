using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Dto;
using Fitichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class FTasks : Entity
    {
        [BsonElement("starts")]
        public DateTime StartDate { get; set; }
        [BsonElement("parent")]
        public ObjectId? Parent { get; set; }
        [BsonElement("subtasks")]
        public List<ObjectId> Subtasks { get; set; } = new();
        [BsonElement("employees")]
        public List<Employee> EmployeesAssigned { get; set; } = new();
        [BsonElement("material")]
        public List<Material> Material { get; set; } = new();
        [BsonElement("address")]
        public Address? Address { get; set; }

        public FTasks(NewFTaskDto newTask) : base(newTask.Name, newTask.Closed)
        {
            EmployeesAssigned = newTask.Assignees;
            if (newTask.Address is not null) Address = newTask.Address;
            if (newTask.Parent is not null) Parent = newTask.Parent;
        }

        public void Update(UpdateFTaskDto data)
        {
            if (data.Name is not null) Name = data.Name;
            if (data.StartDate is not null) StartDate = (DateTime)data.StartDate;
            if (data.Subtasks is not null) Subtasks = data.Subtasks;
            if (data.EmployeesAssigned is not null) EmployeesAssigned = data.EmployeesAssigned;
            if (data.Material is not null) Material = data.Material;
            if (data.Address is not null) Address = data.Address;
            if (data.Closed is not null) Closed = data.Closed;
        }
    }
}