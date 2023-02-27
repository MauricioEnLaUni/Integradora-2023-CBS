namespace Fictichos.Constructora.DTOs
{
    public record EmployeeInfoDTO
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateOnly DOB { get; set; }
        public List<JobInfoDTO> Charges { get; set; }
        public List<ScheduleInfoDTO> ScheduleHistory { get; set; }
    }
}