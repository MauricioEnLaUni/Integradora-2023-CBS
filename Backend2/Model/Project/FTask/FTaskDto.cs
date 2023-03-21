using MongoDB.Bson;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto
{
    public record NewFTaskDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Parent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Ends { get; set; }
        public List<string> Assignees { get; set; } = new();
        public Address? Address { get; set; }
    }

    public record UpdatedFTaskDto : DtoBase
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Parent { get; set; }
        public List<IndexedObjectUpdate<NewFTaskDto, UpdatedFTaskDto>>? Subtasks
            { get; set; }
        public List<UpdateList<string>>? Material { get; set; }
        public List<UpdateList<string>>? EmployeesAssigned { get; set; }
        public Address? Address { get; set; }
        public DateTime? Ends { get; set; }
    }

    public record FTasksDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime Ends { get; set; }
        public string? Parent { get; set; }
        public List<FTasksDto> Subtasks { get; set; } = new();
        public List<string> EmployeesAssigned { get; set; } = new();
        public List<string> Material { get; set; } = new();
        public Address? Address { get; set; }
    }

    public record UpdatedSubtaskDto : DtoBase
    {
        public int Operation { get; set; }
        public int Key { get; set; }
        public NewFTaskDto? NewTask { get; set; }
        public UpdatedFTaskDto? Task { get; set; }
    }
}