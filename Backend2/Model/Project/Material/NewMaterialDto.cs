using System.ComponentModel.DataAnnotations;

using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
    public record NewMaterialDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        public NewAddressDto Location { get; set; } = new();
        public int? Status { get; set; } = 0;
        [Required]
        public double BoughtFor { get; set; } = 0;
    }
}