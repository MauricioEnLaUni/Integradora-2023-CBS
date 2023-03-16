using MongoDB.Bson;

using Fictichos.Constructora.Model;

namespace Fictichos.Constructora.Dto
{
    public record NewFTaskDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ObjectId? Parent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Closed { get; set; }
        public List<ObjectId> Assignees { get; set; } = new();
        public Address? Address { get; set; }
    }

    public record UpdatedFTaskDto : DtoBase
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public List<UpdatedSubtaskDto>? Subtasks { get; set; }
        public List<UpdateEmbeddedDto>? Material { get; set; }
        public List<UpdateEmbeddedDto>? EmployeesAssigned { get; set; }
        public Address? Address { get; set; }
        public DateTime? Closed { get; set; }
    }

    public record FTasksDto
    {
        public ObjectId Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Closed { get; set; }
        public ObjectId? Parent { get; set; }
        public List<FTasksDto> Subtasks { get; set; } = new();
        public List<ObjectId> EmployeesAssigned { get; set; } = new();
        public List<ObjectId> Material { get; set; } = new();
        public Address? Address { get; set; }
    }

    public record UpdatedSubtaskDto : DtoBase
    {
        public int Operation { get; set; }
        public int Key { get; set; }
        public NewFTaskDto? NewTask { get; set; }
        public UpdatedFTaskDto? Task { get; set; }
    }

    public record UpdateEmbeddedDto
    {
        public int Operation { get; set; }
        public ObjectId Data { get; set; } = new();
    }
}