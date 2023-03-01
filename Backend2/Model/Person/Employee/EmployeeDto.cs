using System.ComponentModel.DataAnnotations;

namespace Fitichos.Constructora.Dto
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
}