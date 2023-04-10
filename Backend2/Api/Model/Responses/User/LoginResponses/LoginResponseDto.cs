using System.Security.Claims;

namespace Fictichos.Constructora.Dto;
public record LoginSuccessDto : DtoBase
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Owner { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public List<string> Email { get; set; } = new();
    public byte[]? Avatar { get; set; }
    public List<Claim> Credentials { get; set; } = new();
}