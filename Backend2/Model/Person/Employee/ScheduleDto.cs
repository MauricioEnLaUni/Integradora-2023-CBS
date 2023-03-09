using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto
{
    public record NewScheduleDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Dictionary<TimeSpan, int> Hours { get; set; } = new();
        public NewAddressDto? Location { get; set; }
    }
}