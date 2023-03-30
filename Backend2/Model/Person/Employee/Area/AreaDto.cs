namespace Fictichos.Constructora.Dto;

public record NewAreaDto
{
    public string Name { get; set; } = string.Empty;
    public string Head { get; set; } = string.Empty;
    public string Collection { get; set; } = string.Empty;
}

public record UpdatedAreaDto : DtoBase
{
    public string? Name { get; set; }
    public string? Head { get; set; }
    public string? Collection { get; set; }
}

public record AreaDto
{
    public string Name { get; set; } = string.Empty;
    public string Head { get; set; } = string.Empty;
}