using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto;

public record NewUserDto
{
    [Required]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_]{3,27}$")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$")]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Owner { get; set; } = string.Empty;
}