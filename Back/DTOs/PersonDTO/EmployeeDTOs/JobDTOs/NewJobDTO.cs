namespace Fictichos.Constructora.DTOs
{
    public record NewJobDTO
    {
        public string Name { get; set;}
        public SalaryInfoDTO SalaryHistory { get; set; }
        public string Role { get; set; }
        public string Area { get; set; }
        public List<string> Responsibilities { get; set; }
    }
}