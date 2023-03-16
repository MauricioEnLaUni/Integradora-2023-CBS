using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

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
    public record ScheduleDto
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Dictionary<TimeSpan, int> Hours { get; set; } = new();
        public AddressDto? Location { get; set; }
    }

    public record UpdatedScheduleDto
    {
        [Required]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public Dictionary<TimeSpan, int>? Hours { get; set; }
        public NewAddressDto? Address { get; set; }
    }
}