using System.ComponentModel.DataAnnotations;

using Fictichos.Constructora.Model;

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
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string[] Email { get; set; }
        public byte[]? Avatar;

        public LoginSuccessDto(User usr)
        {
            Id = usr.Id.ToString();
            Name = usr.Name;
            Password = usr.Password;
            CreatedAt = usr.CreatedAt;
            Email = usr.Email.ToArray<string>();
            if (usr.Avatar is not null) Avatar = usr.Avatar;
        }
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

    public record UserChangesDto : DtoBase
    {
        public string? Password { get; set; } = string.Empty;
        public List<string>? Email { get; set; }
        public byte[]? Avatar { get; set; }

        public UserChangesDto(User usr)
        {
            Password = usr.Password;
            Email = usr.Email;
            Avatar = usr.Avatar;
        }
    }

    public record UserEmailDto : DtoBase
    {
        public string Email { get; set; } = string.Empty;
        public bool method = false;
    }
}