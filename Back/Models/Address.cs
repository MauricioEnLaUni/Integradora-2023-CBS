using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Address
    {
        [BsonElement("street")]
        private string? Street { get; set; }
        [BsonElement("number")]
        private string? Number { get; set; }
        [BsonElement("colony")]
        private string? Colony { get; set; }
        [BsonElement("postalCode")]
        private string? PostalCode { get; set; }
        [BsonElement("city")]
        private string? City { get; set; }
        [BsonElement("state")]
        private string? State { get; set; }
        [BsonElement("country")]
        private string? Country { get; set; }
        [BsonElement("latitude")]
        private string? Latitude { get; set; }
        [BsonElement("longitude")]
        private string? Longitude { get; set; }

        public Address(string[] args)
        {
            Street = args[0];
            Number = args[1];
            Colony = args[2];
            PostalCode = args[3];
            City = args[4];
            State = args[5];
            Country = args[6];
            Latitude = args[7];
            Longitude = args[8];
        }
    }
}