using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class Address
    {
        [BsonElement("street")]
        public string? Street { get; set; }
        [BsonElement("number")]
        public string? Number { get; set; }
        [BsonElement("colony")]
        public string? Colony { get; set; }
        [BsonElement("postalCode")]
        public string? PostalCode { get; set; }
        [BsonElement("city")]
        public string? City { get; set; }
        [BsonElement("state")]
        public string? State { get; set; }
        [BsonElement("country")]
        public string? Country { get; set; }
        [BsonElement("coor")]
        public Coordinates? Coordinates { get; set; }

        public Address(NewAddressDto data)
        {
            Street = data.Street ?? null;
            Number = data.Number ?? null;
            Colony = data.Colony ?? null;
            PostalCode = data.PostalCode ?? null;
            City = data.City ?? null;
            State = data.State ?? null;
            Country = data.Country ?? null;
            Coordinates = data.Coordinates ?? null;
        }

        public string AsDto()
        {
            AddressDto data = new(this);
            return JsonConvert.SerializeObject(data);
        }

        public string AsSlimDto()
        {
            SlimAddressDto data = new(this);
            return JsonConvert.SerializeObject(data);
        }
    }
    public class Coordinates
    {
        [BsonElement("latitude")]
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")]
        public string Latitude { get; set; } = string.Empty;
        [BsonElement("longitude")]
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")]
        public string Longitude { get; set; } = string.Empty;

        public string Stringify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}