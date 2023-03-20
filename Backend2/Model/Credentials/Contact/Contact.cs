using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Contact : BaseEntity,
        IQueryMask<Contact, NewContactDto, UpdatedContactDto>
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
        
        public Contact Instantiate(NewContactDto data)
        {
            return new(data);
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

        public void Update(UpdatedContactDto data)
        {
            if (data.Addresses is not null)
            {
                data.Addresses.ForEach(Addresses.UpdateObjectWithIndex);
            }
            if (data.Phones is not null)
            {
                data.Phones.ForEach(Phones.UpdateWithIndex);
            }
            if (data.Emails is not null)
            {
                data.Emails.ForEach(Emails.UpdateWithIndex);
            }
        }

        public string Serialize()
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