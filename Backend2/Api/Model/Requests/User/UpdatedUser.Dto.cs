using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;
public record UpdatedUserDto : DtoBase
{
    public string token = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public bool? Active { get; set; }
    public List<UpdateList<string>>? Email { get; set; }
    public List<UpdateList<string>>? Roles { get; set; }
    public byte[]? Avatar { get; set; }
    public DateTime? Closed { get; set; }
}