using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto
{
    public record NewContactDto
    {
        public NewAddressDto? Addresses { get; set; } = new();
        public string? Phones { get; set; } = string.Empty;
        public string? Emails { get; set; } = string.Empty;
    }
}