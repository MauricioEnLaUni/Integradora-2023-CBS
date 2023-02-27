namespace Fictichos.Constructora.DTOs
{
    public record NewContactDTO
    {
        public string Id { get; set; }
        public List<AddressInfoDTO> Addresses { get; set; }
        public List<string> Phones { get; set; }
        public List<string> Emails { get; set; }
    }
}