namespace Fictichos.Constructora.DTOs
{
    public record NewEmployeeDTO
    {
        public string Name { get; set; }
        public DateOnly DOB { get; set; }
        public string CURP { get; set; }
        public NewJobDTO Charges { get; set; }
    }
}