namespace Fictichos.Constructora.DTOs
{
    public record UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public List<string> Email { get; set; }
    }
}