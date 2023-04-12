using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class ExternalPerson : BaseEntity,
        IQueryMask<ExternalPerson, NewExPersonDto, UpdatedExPersonDto>
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get;  set; } = string.Empty;
        public string Position { get; private set; } = string.Empty;
        public string Area { get; private set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
        public Contact Contacts { get; set; } = new();

        // Subcontractors

        public ExternalPerson() { }

        public ExternalPerson(NewExPersonDto data)
        {
            Name = data.Name;
            LastName = data.LastName;
            Position = data.Position;
            Area = data.Area;
            Role = data.Role;
            Project = data.Project;

            if (data.Email is not null) Contacts.Emails.Add(data.Email);
            if (data.Phone is not null) Contacts.Phones.Add(data.Phone);
        }

        public ExternalPerson Instantiate(NewExPersonDto data)
        {
            return new(data);
        }

        public ExternalPersonDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                LastName = LastName,
                Area = Area,
                Position = Position,
                Contact = Contacts.ToDto()
            };
        }

        internal PersonCondensedDto ToCondensed()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Position = Position
            };
        }

        public string Serialize()
        {
            ExternalPersonDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedExPersonDto data)
        {
            Name = data.Name ?? Name;
            LastName = data.LastName ?? LastName;
            if (data.Contacts is not null) Contacts.Update(data.Contacts);
            
            Area = data.Area;
            Role = data.Role;
            Project = data.Project;
        }
    }
}