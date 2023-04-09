using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Dto;

public record NewRecordDto
{
    public string Event { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Starts { get; set; }
    public DateTime Ends { get; set; }
}

public record UpdatedRecordDto : DtoBase
{
    public string? Event { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime? Starts { get; set; }
    public DateTime? Ends { get; set; }
}

public record RecordDto()
{
    public string Id { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Starts { get; set; }
    public DateTime Ends { get; set; }
}