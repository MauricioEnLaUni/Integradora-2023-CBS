using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Person : Entity,
        IQueryMask<Person, PersonDto, UpdatedPersonDto>
    {
        [BsonElement("lastName")]
        public string LastName { get;  set; } = string.Empty;
        [BsonElement("contact")]
        public Contact Contacts { get; set; } = new();
        [BsonElement("isEmployed")]
        public Employee? Employed { get;  set; } = new();

        public Person() { }

        public Person(NewPersonDto data)
        {
            LastName = data.LastName;
            Contacts = new Contact();
            if (data.Email is not null) Contacts.Emails.Add(data.Email);
            if (data.Phone is not null) Contacts.Phones.Add(data.Phone);
            if (data.IsEmployee is not null) Employed = new(data.IsEmployee);
        }

        public Person FakeConstructor(string dto)
        {
            try
            {
                return new Person (JsonConvert
                    .DeserializeObject<NewPersonDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public PersonDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Contact = Contacts.ToDto(),
                Employee = Employed?.ToDto()
            };
        }

        public string SerializeDto()
        {
            PersonDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedPersonDto data)
        {
            Name = data.Name ?? Name;
            LastName = data.LastName ?? LastName;
            if (data.Contacts is not null) Contacts.Update(data.Contacts);
            if (data.Employed is not null)
            {
                Employed ??= new();
                Employed.Update(data.Employed);
            }
        }
    }
}