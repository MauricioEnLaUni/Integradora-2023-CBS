namespace Fictichos.Constructora.Dto;

public record BrowserUpdateCompanyDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;
    public string Relation { get; set; } = string.Empty;
}