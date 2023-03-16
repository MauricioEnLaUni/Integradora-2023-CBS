using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto
{
    public record NewContactDto
    {
        public NewAddressDto? Addresses { get; set; } = new();
        public string? Phones { get; set; } = string.Empty;
        public string? Emails { get; set; } = string.Empty;
    }

    public record ContactDto
    {
        public List<AddressDto> Addresses { get; init; } = new();
        public List<string> Phones { get; init; } = new();
        public List<string> Emails { get; init; } = new();
    }

    public record UpdatedContactDto : DtoBase
    {
        public List<NewAddressDto>? Addresses { get; init; } = new();
        public List<UpdatedPhone> Phones { get; init; } = new();
        public List<UpdatedEmail> Emails { get; init; } = new();
    }

    public record UpdatedPhone
    {
        public int? Index { get; set; }
        public int Operation { get; set; }
        public string Data { get; set; } = string.Empty;
    }

    public record UpdatedEmail
    {
        public int? Index { get; set; }
        public int Operation { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}