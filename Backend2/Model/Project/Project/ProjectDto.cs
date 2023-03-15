using MongoDB.Bson;

using Fictichos.Constructora.Model;

namespace Fictichos.Constructora.Dto
{
    public record ProjectDto
    {
        public ObjectId Responsible { get; }
        public AccountDto? PayHistory { get; }
        public List<FTasksDto> Tasks { get; } = new();
    }

    public record NewProjectDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public record ProjectChangesDto : DtoBase
    {
        public string? Name { get; set; }
        public List<FTasks>? Tasks { get; set; }
        public DateTime? Closed { get; set; }
        public Account? PayHistory { get; set; }
    }
}