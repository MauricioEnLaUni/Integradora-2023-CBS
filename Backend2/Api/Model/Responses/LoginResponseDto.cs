using Fictichos.Constructora.Model;

namespace Fictichos.Constructora.Dto;

public class LoginResponseDto
{
    public string id = string.Empty;
    public string token = string.Empty;
    public string name = string.Empty;
    public DateTime createdAt = DateTime.Now;
    public List<string> roles = new();
    public byte[]? avatar = Array.Empty<byte>();

    public LoginResponseDto(User data, string Token)
    {
        id = data.Id;
        token = Token;
        name = data.Name;
        createdAt = data.CreatedAt;
        roles = data.Roles;
        avatar = data.Avatar;
    }
}