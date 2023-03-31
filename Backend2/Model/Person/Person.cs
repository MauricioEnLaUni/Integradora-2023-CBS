using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Person : BaseEntity,
        IQueryMask<Person, NewPersonDto, UpdatedPersonDto>
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get;  set; } = string.Empty;
        public string Relation { get; private set; } = string.Empty;
        public Contact Contacts { get; set; } = new();
        public Employee? Employed { get;  set; } = new();

        public Person() { }

        public Person(NewPersonDto data)
        {
            Name = data.Name;
            LastName = data.LastName;
            Contacts = new Contact();
            if (data.Email is not null) Contacts.Emails.Add(data.Email);
            if (data.Phone is not null) Contacts.Phones.Add(data.Phone);
            if (data.IsEmployee is not null) Employed = new(data.IsEmployee);
        }

        public Person Instantiate(NewPersonDto data)
        {
            return new(data);
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

        public string Serialize()
        {
            PersonDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedPersonDto data)
        {
            Name = data.Name ?? Name;
            LastName = data.LastName ?? LastName;
            if (data.Contacts is not null) Contacts.Update(data.Contacts);

            if (data.Employed is null) return;
            Employed ??= new();
            Employed.Update(data.Employed);
        }
    }
}