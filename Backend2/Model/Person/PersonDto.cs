using System.ComponentModel.DataAnnotations;

using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto
{
    public record NewPersonDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        [RegularExpression(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$")]
        public string RFC { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)")]
        public string CURP { get; set; } = string.Empty;
        [Required]
        public NewJobDto Job { get; set; } = new();
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }

    public record PersonDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CurrentJob { get; set; } = string.Empty;
        public ContactDto Contact { get; set; } = new();
    }

    public record UpdatedPersonDto : DtoBase
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        [RegularExpression(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$")]
        public string? RFC { get; set; }
        [RegularExpression(@"([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)")]
        public string? CURP { get; set; }
        public List<IndexedObjectUpdate<NewJobDto, UpdatedJobDto>>?
            Charges { get; set; }
        public List<IndexedObjectUpdate<NewScheduleDto, UpdatedScheduleDto>>?
            ScheduleHistory { get; set; }
        public List<IndexedObjectUpdate<NewRecordDto, UpdatedRecordDto>>?
            Historial { get; set; }
        public UpdatedContactDto? Contacts { get; set; }
        public string? Username { get; set; }
    }
}