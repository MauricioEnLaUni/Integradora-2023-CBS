namespace Fictichos.Constructora.Dto;

public record TokenDto
{
    public string Id { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
}