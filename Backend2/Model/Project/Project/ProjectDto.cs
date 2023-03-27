using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto
{
    public record ProjectDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public FTasksDto? LastTask { get; set; } = new();
    }

    public record NewProjectDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public record UpdatedProjectDto : DtoBase
    {
        public string? Name { get; set; }
        public string? Responsible { get; set; }
        public List<IndexedObjectUpdate<NewFTaskDto, UpdatedFTaskDto>>? Tasks
            { get; set; }
        public DateTime? Starts { get; set; }
        public DateTime? Ends { get; set; }
        public UpdatedAccountDto? PayHistory { get; set; }
        public Account? Transferred { get; set; }
    }
}