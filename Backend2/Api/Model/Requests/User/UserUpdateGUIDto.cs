using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;

public record UserSelfUpdateDto
{
    public string token = string.Empty;
    public string? password;
    public byte[]? avatar;
    public List<UpdateList<string>>? email;
    public bool? killAccount;
}

public record UserAdminUpdateDto
{
    public string name = string.Empty;
    public UserSelfUpdateDto basicFields = new();
    public List<UpdateList<string>>? roles;
}