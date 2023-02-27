using MongoDB.Bson;

namespace Fictichos.Constructora.DTOs
{
    public record UpdateEmployeeDTO
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? DOB { get; set; }
        public List<JobInfoDTO>? Charges { get; set; }
        public List<ScheduleInfoDTO>? ScheduleHistory { get; set; }
    }
}