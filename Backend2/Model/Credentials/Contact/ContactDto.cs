using Fictichos.Constructora.Utilities;

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
        public List<IndexedObjectUpdate<NewAddressDto, NewAddressDto>>?
            Addresses { get; init; } = new();
        public List<UpdateList<string>>? Phones { get; init; } = new();
        public List<UpdateList<string>>? Emails { get; init; } = new();
    }
}