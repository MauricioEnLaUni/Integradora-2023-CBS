using System.ComponentModel.DataAnnotations;

namespace Fitichos.Constructora.Dto
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
}