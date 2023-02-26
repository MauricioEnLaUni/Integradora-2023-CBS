namespace Fictichos.Constructora.DTOs
{
    /// <summary>
    /// Defines the info a user can see of themselves.
    /// </summary>
    public record UserInfoDTO
    {
        public string Username { get; set; }
        public List<string> Email { get; set; }
        public byte[]? Avatar { get; set; }
        public DateTime Created { get; set; }
    }
}