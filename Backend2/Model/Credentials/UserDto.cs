using System.ComponentModel.DataAnnotations;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto
{
    public record LoginDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_]{3,27}$")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$")]
        public string Password { get; set; } = string.Empty;
    }

    public record LoginSuccessDto : DtoBase
    {
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<string> Email { get; set; } = new();
        public byte[]? Avatar;
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
        [RegularExpression(@"^[\\.-\\w]{1,25}@cbs.com$")]
        public string Email { get; set; } = string.Empty;
    }

    public record UpdatedUserDto : IUpdateDto<User>
    {
        public new Dictionary<string, dynamic> Changes { get; set; } = new();
    }

    public record UserEmailDto : DtoBase
    {
        public string Email { get; set; } = string.Empty;
        public bool method = false;
    }
}