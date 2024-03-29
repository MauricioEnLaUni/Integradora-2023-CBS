using Newtonsoft.Json;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Address : BaseEntity,
        IQueryMask<Address, NewAddressDto, NewAddressDto>
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

        public Address() { }
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

        public Address Instantiate(NewAddressDto data)
        {
            return new Address(data);
        }

        public void Update(NewAddressDto data)
        {
            ModifiedAt = DateTime.Now;
            Street = data.Street ?? null;
            Number = data.Number ?? null;
            Colony = data.Colony ?? null;
            PostalCode = data.PostalCode ?? null;
            City = data.City ?? null;
            State = data.State ?? null;
            Country = data.Country ?? null;
            Coordinates = data.Coordinates ?? null;
        }

        public AddressDto ToDto()
        {
            return new(this);
        }

        public string Serialize()
        {
            AddressDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public string ToSlimDto()
        {
            SlimAddressDto data = new(this);
            return JsonConvert.SerializeObject(data);
        }
    }
}