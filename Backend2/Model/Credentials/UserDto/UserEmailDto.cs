using Fitichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
    public record UserEmailDto : DtoBase
    {
        public string Email { get; set; } = string.Empty;
        public bool method = false;
    }
}