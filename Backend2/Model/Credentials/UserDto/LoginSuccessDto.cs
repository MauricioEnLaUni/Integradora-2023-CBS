using Fitichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
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
}