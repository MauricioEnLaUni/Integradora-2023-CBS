using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Dto;
using Fitichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Person : Entity
    {
        [BsonElement("lastName")]
        public string LastName { get;  set; }
        [BsonElement("contact")]
        public Contact Contacts { get; set; } = new Contact();
        [BsonElement("isEmployed")]
        public Employee? Employed { get;  set; } 

        public Person(NewPersonDto data) : base(data.Name, null)
        {
            LastName = data.LastName;
            Contacts = new Contact();
            if (data.Email is not null) Contacts.Emails.Add(data.Email);
            if (data.Phone is not null) Contacts.Phones.Add(data.Phone);
            if (data.IsEmployee is not null) Employed = new(data.IsEmployee);
        }
    }
}