namespace Fictichos.Constructora.DTOs
{
    public record UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}