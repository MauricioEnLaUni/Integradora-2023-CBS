using System.ComponentModel.DataAnnotations;
using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
    public record NewEmployeeDto
    {
        public string Name { get; set;} = string.Empty;
        public DateOnly DOB { get; set; }
        public string CURP { get; set; } = string.Empty;
        public NewJobDto Charges { get; set; } = new();
    }
}