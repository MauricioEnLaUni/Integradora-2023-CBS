using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.DTOs
{
    public record UpdatedUserDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_]{4,27}$")]
        public string Username { get; set; }
        public string? Password { get; set; }
        public byte[]? Avatar { get; set; }
        public bool? Active { get; set; }
        public List<string>? Email { get; set; }
    }
}