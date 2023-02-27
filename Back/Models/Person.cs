using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
{
    public class Person : Entity
    {
        [BsonElement("lastName")]
        public string LastName { get;  set; }
        [BsonElement("contact")]
        public Contact Contacts { get; set; } = new Contact();
        [BsonElement("isEmployed")]
        public Employee? Employed { get;  set; } 

        public Person(string name, string last, Employee? job) : base(name)
        {
            LastName = last;
            if (job != null) Employed = job;
        }

        public Person(NewPersonDTO data) : base(data.Name)
        {
            LastName = data.LastName;
            Contacts = new Contact();
            if (data.Email is not null) Contacts.Emails.Add(data.Email);
            if (data.Phone is not null) Contacts.Phones.Add(data.Phone);
            if (data.IsEmployee is not null) Employed = data.IsEmployee;
        }

        public PersonInfoDTO AsDTO()
        {
            return new PersonInfoDTO
            {
                Name = this.Name,
                LastName = this.LastName
            };
        }
    }
}