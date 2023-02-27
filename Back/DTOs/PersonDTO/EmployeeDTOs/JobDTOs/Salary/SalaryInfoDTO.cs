namespace Fictichos.Constructora.DTOs
{
    public record SalaryInfoDTO
    {
        public DateTime Created { get; set; }
        public Dictionary<string, double> Reductions { get; set;}
        public double Rate { get; set; }
        public int? HoursWeek { get; set;}
    }
}