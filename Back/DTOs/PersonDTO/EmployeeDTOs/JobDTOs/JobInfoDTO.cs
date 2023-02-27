namespace Fictichos.Constructora.DTOs
{
    public record JobInfoDTO
    {
        public string Name { get; set;}
        public List<SalaryInfoDTO> SalaryHistory { get; set; }
        public string Role { get; set; }
        public string Area { get; set; }
        public List<string> Responsibilities { get; set; }
    }
}