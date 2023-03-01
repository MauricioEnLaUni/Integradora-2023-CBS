using System.ComponentModel.DataAnnotations;

namespace Fitichos.Constructora.Dto
{
    public record NewSalaryDto
    {
        [Required]
        public double Rate { get; set; } = 0;        
    }
}