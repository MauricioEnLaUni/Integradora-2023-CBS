using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;

namespace Fictichos.Constructora.Dto
{
    public record NewPersonDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [RegularExpression(@"^\\+?[1-9][0-9]{7,14}$")]
        public string? Phone { get; set; } = string.Empty;
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public string? Email { get; set; } = string.Empty;
        public NewEmployeeDto? IsEmployee { get; set; }
    }

    public record PersonDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ContactDto Contact { get; set; } = new();
        public EmployeeDto? Employee { get; set; }
    }

    public record UpdatedPersonDto : DtoBase
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public UpdatedContactDto? Contacts { get; set; }
        public UpdatedEmployeeDto? Employed { get; set; }
    }
}