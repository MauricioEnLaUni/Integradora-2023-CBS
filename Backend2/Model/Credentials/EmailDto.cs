namespace Fictichos.Constructora.Dto;

public record NewEmailDto
{
    internal string owner = string.Empty;
    internal string value = string.Empty;
}

public record UpdatedEmailDto : DtoBase
{
    internal string? owner;
    internal string? value;
}

public record EmailContainerDto
{
    public string email = string.Empty;
}