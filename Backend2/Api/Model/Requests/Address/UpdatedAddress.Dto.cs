using System.ComponentModel.DataAnnotations;
using Fictichos.Constructora.Model;

namespace Fictichos.Constructora.Dto;

public record NewAddressDto : DtoBase
{
    [StringLength(32)]
    public string? Street { get; set; }
    [StringLength(10)]
    public string? Number { get; set; }
    [StringLength(32)]
    public string? Colony { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public Coordinates? Coordinates { get; set; }
}