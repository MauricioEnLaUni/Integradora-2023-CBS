using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.DTOs
{
    public record NewPersonDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Employee? IsEmployee { get; set; }
    }
}