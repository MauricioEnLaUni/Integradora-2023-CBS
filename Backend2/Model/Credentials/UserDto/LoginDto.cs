namespace Fitichos.Constructora.Repository
{
    public record LoginDto
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}