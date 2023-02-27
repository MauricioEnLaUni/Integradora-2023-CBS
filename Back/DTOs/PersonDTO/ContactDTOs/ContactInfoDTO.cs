namespace Fictichos.Constructora.DTOs
{
    public record ContactInfoDTO
    {
        public List<AddressInfoDTO> Addresses { get; set; }
        public List<string> Phones { get; set; }
        public List<string> Emails { get; set; }
    }
}