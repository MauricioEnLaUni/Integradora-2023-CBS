using System.ComponentModel.DataAnnotations;

namespace Fitichos.Constructora.Dto
{
    public record NewAddressDto
    {
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Colony { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")]
        public string? Latitude { get; set; }
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")]
        public string? Longitude { get; set; }
    }
}