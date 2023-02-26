using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.DTOs
{
    public record NewUserDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_]{4,27}$")]
        public string Username { get; init; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$")]
        public string Password { get; init; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@cbs.com$")]
        public string Email { get; init; }
    }
}