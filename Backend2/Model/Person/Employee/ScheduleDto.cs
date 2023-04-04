using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Fictichos.Constructora.Dto
{
    public record NewScheduleDto
    {
        [Required]
        public string Period { get; set; } = string.Empty;
        [Required]
        public Dictionary<TimeSpan, int> Hours { get; set; } = new();
        public NewAddressDto? Location { get; set; }
    }
    public record ScheduleDto
    {
        public string Id { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public AddressDto? Location { get; set; }
    }

    public record UpdatedScheduleDto : DtoBase
    {
        public string? Period { get; set; }
        public Dictionary<TimeSpan, int>? Hours { get; set; }
        public NewAddressDto? Location { get; set; }
    }
}