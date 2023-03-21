using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;

public record UserUpdateGUIDto
{
    public string token = string.Empty;
    public string? password = string.Empty;
    public List<UpdateList<string>>? email = new();
    public List<UpdateList<string>>? roles = new();
    public byte[]? avatar = Array.Empty<byte>();
}

public record UserUpdateAdminDto
{
    public string token = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public bool? Active { get; set; }
    public List<UpdateList<string>>? Email { get; set; }
    public List<UpdateList<string>>? Roles { get; set; }
    public UpdatedCredentialsDto? Credentials { get; set; }
    public byte[]? Avatar { get; set; }
    public DateTime? Closed { get; set; }
}