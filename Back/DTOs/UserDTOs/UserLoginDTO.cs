using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.DTOs
{
    public record UserLoginDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_]{4,27}$")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}