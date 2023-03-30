using System.ComponentModel.DataAnnotations;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto
{
    public record NewJobDto
    {
        [Required]
        public NewSalaryDto SalaryHistory { get; set; } = new();
        [Required]
        public string Role { get; set; } = string.Empty;
        [Required]
        public string Area { get; set; } = string.Empty;
        public string Parent { get; set; } = string.Empty;
        [Required]
        public List<string> Responsibilities { get; set; } = new();
    }

    public record JobDto : DtoBase
    {
        public string Name { get; set; } = string.Empty;
        public string InternalKey { get; set; } = string.Empty;
        public List<SalaryDto> SalaryHistory { get; set; } = new();
        public string Role { get; set; } = string.Empty;
    }

    public record UpdatedJobDto : DtoBase
    {
        public string? Role { get; set; }
        public string? Area { get; set; }
        public List<UpdateList<string>>? Oversees { get; set; }
        public string? Parent { get; set; }
        public List<UpdateList<string>>? Responsibilities { get; set; }
        public List<UpdateList<string>>? Material { get; set; }
    }
}