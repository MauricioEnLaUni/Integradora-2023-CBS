using System.ComponentModel.DataAnnotations;
using Fictichos.Constructora.Model;

namespace Fictichos.Constructora.Dto
{
    public record NewAddressDto
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

    public record AddressDto
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
}