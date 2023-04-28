using System.ComponentModel.DataAnnotations;
using Fictichos.Constructora.Model;

namespace Fictichos.Constructora.Dto;
public record AddressDto : DtoBase
{
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Colony { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

    public AddressDto(Address data)
    {
        Street = data.Street;
        Number = data.Number;
        Colony = data.Colony;
        PostalCode = data.PostalCode;
        City = data.PostalCode;
        State = data.State;
        Country = data.Country;
    }
    public AddressDto() { }
}

public record SlimAddressDto
{
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Colony { get; set; }
    public string? PostalCode { get; set; }

    public SlimAddressDto(Address data)
    {
        Street = data.Street;
        Number = data.Number;
        Colony = data.Colony;
        PostalCode = data.PostalCode;
    }
}