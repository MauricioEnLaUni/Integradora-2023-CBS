using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto
{
    public record NewSalaryDto
    {
        [Required]
        public double Rate { get; set; } = 0;        
    }
}