using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;

namespace Fictichos.Constructora.Dto
{
    public record NewEmployeeDto
    {
        [Required]
        public string Name { get; set;} = string.Empty;
        [Required]
        public DateOnly DOB { get; set; }
        [Required]
        [RegularExpression(@"([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$")]
        public string CURP { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$")]
        public string RFC { get; set; } = string.Empty;
        [Required]
        public NewJobDto Charges { get; set; } = new();
    }
    public record EmployeeDto
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DOB { get; set; }
        public string CURP { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public List<JobDto> Charges { get; set; } = new();
        public List<ScheduleDto> ScheduleHistory { get; set; } = new();
    }

    public record UpdatedEmployeeDto : DtoBase
    {
        public string? Name { get; set; }
        public DateOnly? DOB { get; set; }
        public string? CURP { get; set; }
        public string? RFC { get; set; }
        public List<UpdateCharges>? Charges { get; set; }
        public List<UpdateScheduleDto>? ScheduleHistory { get; set; }
        public DateTime? Closed { get; set; }
        public bool? Active { get; set; }
    }

    public record UpdateCharges
    {
        public int Operation { get; set; }
        public int Key { get; set; }
        public NewJobDto? NewData { get; set; } = new();
        public UpdatedJobDto? UpdatedData { get; set; } = new();
    }

    public record UpdateScheduleDto
    {
        public int Operation { get; set; }
        public int Key { get; set; }
        public NewScheduleDto? NewData { get; set; } = new();
        public UpdatedScheduleDto? UpdatedData { get; set; } = new();
    }
}