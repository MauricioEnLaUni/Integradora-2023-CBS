using MongoDB.Bson;

using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
    public record NewFTaskDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ObjectId? Parent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Closed { get; set; }
        public List<Employee> Assignees { get; set; } = new();
        public Address? Address { get; set; }
    }

    public record UpdateFTaskDto
    {
        public string Owner { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public List<ObjectId>? Subtasks { get; set; }
        public List<Employee>? EmployeesAssigned { get; set; }
        public List<Material>? Material { get; set; }
        public Address? Address { get; set; }
        public DateTime? Closed { get; set; }
    }
}