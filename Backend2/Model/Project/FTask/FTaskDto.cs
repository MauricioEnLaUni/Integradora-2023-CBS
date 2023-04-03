using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;
public record NewFTaskDto
{
    public string Name { get; set; } = string.Empty;
    public string? Parent { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime Ends { get; set; }
    public string Overseer { get; set; } = string.Empty;
    public List<string> Assignees { get; set; } = new();
    public Address? Address { get; set; }
    public string Owner { get; set; } = string.Empty;
}

public record UpdatedFTaskDto : DtoBase
{
    public string Parent { get; set; } = string.Empty;
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public List<IndexedObjectUpdate<NewFTaskDto, UpdatedFTaskDto>>? Subtasks
        { get; set; }
    public List<UpdateList<string>>? Material { get; set; }
    public string? Overseer { get; set; }
    public List<UpdateList<string>>? EmployeesAssigned { get; set; }
    public Address? Address { get; set; }
    public DateTime? Ends { get; set; }
    public bool? Complete { get; set; }
}

public record TaskSingleUpdateList<T> : DtoBase
{
    public List<UpdateList<T>> changes = new();
}

public record TaskSingleUpdate<T> : DtoBase
{
    public T change;
    TaskSingleUpdate(T data)
    {
        change = data;
    }
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

public record TaskProjectDto
{
    public FTasks task = new();
    public Project project = new();
}