using System.ComponentModel.DataAnnotations;

using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
    public record NewContactDto
    {
        public List<NewAddressDto> Addresses { get; set; } = new();
        public List<string> Phones { get; set; } = new();
        public List<string> Emails { get; set; } = new();
    }
}