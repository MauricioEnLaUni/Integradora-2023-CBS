using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Contact : Entity, IQueryMask<Contact, ContactDto>
    {
        [BsonElement("addresses")]
        public List<Address> Addresses { get; private set; } = new List<Address>();
        [BsonElement("phones")]
        public List<string> Phones { get; private set; } = new List<string>();
        [BsonElement("emails")]
        public List<string> Emails { get; private set; } = new List<string>();

        public Contact() { }
        public Contact(NewContactDto data)
        {
            if (data.Addresses is not null)
            {
                Addresses.Add(new(data.Addresses));
            }
            if (data.Phones is not null) Phones.Add(data.Phones);
            if (data.Emails is not null) Emails.Add(data.Emails);
        }
        
        public Contact FakeConstructor(string dto)
        {
            try
            {
                return new Contact(JsonConvert
                    .DeserializeObject<NewContactDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public ContactDto ToDto()
        {
            List<AddressDto> AddressList = new();
            Addresses.ForEach(e => {
                AddressList.Add(new AddressDto(e));
            });
            return new()
            {
                Addresses = AddressList,
                Phones = Phones,
                Emails = Emails
            };
        }

        public string SerializeDto()
        {
            ContactDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public string StringifyPhones()
        {
            return JsonConvert.SerializeObject(Phones);
        }

        public string StringifyEmail()
        {
            return JsonConvert.SerializeObject(Emails);
        }

        public string StringifyAddresses()
        {
            return JsonConvert.SerializeObject(Addresses);
        }
    }
}