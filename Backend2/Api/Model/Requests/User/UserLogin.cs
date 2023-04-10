using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto;

public record LoginDto
{
    [Required]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_]{2,27}$")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$")]
    public string Password { get; set; } = string.Empty;
}