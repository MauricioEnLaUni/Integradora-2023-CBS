using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto
{
    public record NewJobDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public NewSalaryDto SalaryHistory { get; set; } = new();
        [Required]
        public string Role { get; set; } = string.Empty;
        [Required]
        public string Area { get; set; } = string.Empty;
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
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
    }
}