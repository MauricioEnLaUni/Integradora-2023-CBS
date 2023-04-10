using System.ComponentModel.DataAnnotations;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto
{
    public record LoginDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_]{2,27}$")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$")]
        public string Password { get; set; } = string.Empty;
    }

    public record LoginSuccessDto : DtoBase
    {
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Owner { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public List<string> Email { get; set; } = new();
        public byte[]? Avatar { get; set; }
    }

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

    public record UpdatedUserDto : DtoBase
    {
        public string token = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public bool? Active { get; set; }
        public List<UpdateList<string>>? Email { get; set; }
        public List<UpdateList<string>>? Roles { get; set; }
        public byte[]? Avatar { get; set; }
        public DateTime? Closed { get; set; }
    }

    public record UserEmailDto : DtoBase
    {
        public string Email { get; set; } = string.Empty;
        public bool method = false;
    }
}