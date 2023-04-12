using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;

public record CompanyDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;
    public string Relation { get; set; } = string.Empty;
    public ContactDto Contact { get; set; } = new();
}

public record CompanyBrowserDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;
    public string Relation { get; set; } = string.Empty;
}

public record NewCompanyDto
{
    public string Name { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;
    public string Relation { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
}

public record UpdatedCompanyDto : DtoBase
{
    public string Name { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;
    public string Relation { get; set; } = string.Empty;
    public UpdatedContactDto Contacts { get; set; } = new();
    public IndexedObjectUpdate<NewExPersonDto, UpdatedExPersonDto> Members
        { get; set; } = new();
}