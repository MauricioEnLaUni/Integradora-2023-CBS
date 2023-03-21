using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto
{
    public record NewSalaryDto
    {
        public string Period { get; set; } = string.Empty;
        public string Due { get; set; } = string.Empty;
        [Required]
        public double HourlyRate { get; set; } = 0;
        [Required]
        public Dictionary<string, double> Reductions { get; set; } = new();
        [Required]
        public int PayPeriod { get; set; }
        public int? HoursWeeklyCap { get; set; }
    }
    public record SalaryDto : DtoBase
    {
        public Dictionary<string, double> Reductions { get; set; } = new();
        public double HourlyRate { get; set; }
        public string Period { get; set; } = string.Empty;
        public string Due { get; set; } = string.Empty;
        public int? HoursWeeklyCap { get; set; }
    }

    public record UpdatedSalaryDto : DtoBase
    {
        public double? HourlyRate { get; set; }
        public string? Period { get; set; }
        public string? Due { get; set; }
        public Dictionary<string, double>? Reductions { get; set;} = new();
        public int? HoursWeeklyCap { get; set; }
        public DateTime? Closed { get; set; }
    }
}