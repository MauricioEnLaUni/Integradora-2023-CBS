using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
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