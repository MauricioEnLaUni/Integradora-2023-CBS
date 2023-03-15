using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;

namespace Fictichos.Constructora.Dto
{
    public record NewSalaryDto
    {
        [Required]
        public double Rate { get; set; } = 0;
        [Required]
        public Dictionary<string, double> Reductions { get; set; } = new();
        [Required]
        public int PayPeriod { get; set; }
        public int? HoursWeek { get; set; }
    }
    public record SalaryDto
    {
        public ObjectId Id { get; set; }
        public Dictionary<string, double> Reductions { get; set; } = new();
        public double Rate { get; set; }
        public int? HoursWeek { get; set; }
    }
}