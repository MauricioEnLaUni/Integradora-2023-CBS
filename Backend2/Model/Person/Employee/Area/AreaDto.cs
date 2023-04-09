using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;

public record NewAreaDto
{
    public string Name { get; set; } = string.Empty;
    public string? Parent { get; set; } = string.Empty;
    public string? Head { get; set; } = string.Empty;
}

public record UpdatedAreaDto : DtoBase
{
    public string? Name { get; set; }
    public string? Parent { get; set; } = string.Empty;
    public string? Head { get; set; }
    public List<UpdateList<string>>? Children { get; set; } = new();
}

public record AreaDto
{
    public string Name { get; set; } = string.Empty;
    public string? Parent { get; set; } = string.Empty;
    public string? Head { get; set; } = string.Empty;
}