using Fitichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
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
}