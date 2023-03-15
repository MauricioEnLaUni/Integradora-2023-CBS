using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Reports
{
    public record EmployeeReport
    {
        public string Name { get; set; } = string.Empty;
        // Historial administrativo
        public string InternalKey { get; set; } = string.Empty;
        public List<JobDto> Charges { get; set; } = new();
        public List<ScheduleDto> ScheduleHistory { get; set; } = new();
    }

    public record AccountReport
    {

    }

    public record SystemReport
    {

    }

    public record MaterialReport
    {
        
    }
}