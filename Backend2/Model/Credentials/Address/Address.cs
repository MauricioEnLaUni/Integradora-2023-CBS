using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Dto;

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
        [BsonElement("latitude")]
        public string? Latitude { get; set; }
        [BsonElement("longitude")]
        public string? Longitude { get; set; }

        public Address(NewAddressDto data)
        {
            Street = data.Street ?? null;
            Number = data.Number ?? null;
            Colony = data.Colony ?? null;
            PostalCode = data.PostalCode ?? null;
            City = data.City ?? null;
            State = data.State ?? null;
            Country = data.Country ?? null;
            Latitude = data.Latitude ?? null;
            Longitude = data.Longitude ?? null;
        }
    }
}