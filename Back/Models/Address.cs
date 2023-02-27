using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
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

        public Address(AddressInfoDTO data)
        {
            Street = data.Street;
            Number = data.Number;
            Colony = data.Colony;
            PostalCode = data.PostalCode;
            City = data.City;
            State = data.State;
            Country = data.Country;
        }

        public AddressInfoDTO AsDTO()
        {
            return new AddressInfoDTO
            {
                Street = this.Street,
                Number = this.Number,
                Colony = this.Colony,
                PostalCode = this.PostalCode,
                City = this.City,
                State = this.State,
                Country = this.Country
            };
        }
    }
}