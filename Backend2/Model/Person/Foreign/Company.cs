using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    internal class Company : BaseEntity,
        IQueryMask<Company, NewCompanyDto, UpdatedCompanyDto>
    {
        internal string Name { get; set; } = string.Empty;
        internal string Activity { get;  set; } = string.Empty;
        internal string Relation { get; private set; } = string.Empty;
        internal Contact Contacts { get; set; } = new();
        internal List<ExternalPerson> Members { get; set; } = new();

        public Company() { }

        private Company(NewCompanyDto data)
        {
            Name = data.Name;
            Activity = data.Activity;
            Relation = data.Relation;

            if (data.Email is not null) Contacts.Emails.Add(data.Email);
            if (data.Phone is not null) Contacts.Phones.Add(data.Phone);
        }

        public Company Instantiate(NewCompanyDto data)
        {
            return new(data);
        }

        internal CompanyDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Activity = Activity,
                Relation = Relation,
                Contact = Contacts.ToDto()
            };
        }

        public string Serialize()
        {
            CompanyDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedCompanyDto data)
        {
            Name = data.Name ?? Name;
            Activity = data.Activity ?? Activity;
            Relation = data.Relation ?? Relation;
            if (data.Contacts is not null) Contacts.Update(data.Contacts);
        }
    }
}