using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Person : BaseEntity,
        IQueryMask<Person, NewPersonDto, UpdatedPersonDto>
    {
        internal string Name { get; private set; } = string.Empty;
        internal string LastName { get;  private set; } = string.Empty;
        internal Contact Contacts { get; private set; } = new();
        internal DateTime DOB { get; private set; }
        internal string RFC { get; private set; } = string.Empty;
        internal string CURP { get; private set; } = string.Empty;
        internal string InternalKey { get; private set; } = string.Empty;
        internal List<Job> Charges { get; private set; } = new();
        internal List<byte[]> Documents { get; private set; } = new();
        internal List<Schedule> ScheduleHistory { get; private set; } = new();
        internal List<Record> Historial { get; private set; } = new();
        internal string Username { get; private set; } = string.Empty;
        internal double Antiquity { get; private set; } = 0.00;

        public Person() { }

        public Person(NewPersonDto data)
        {
            Name = data.Name;
            LastName = data.LastName;
            DOB = data.DOB;
            RFC = data.RFC;
            CURP = data.CURP;
            GenerateKey();
            Charges.Add(new(data.Job));
            if (data.Email is not null) Contacts.Emails.Add(data.Email);
            if (data.Phone is not null) Contacts.Phones.Add(data.Phone);
        }

        public Person Instantiate(NewPersonDto data)
        {
            return new(data);
        }

        public PersonDto ToDto()
        {
            Job? active = Charges
                .Where(x => x.Active)
                .SingleOrDefault();
            string current = active is null ? "Inactive" : active.Name;
            return new()
            {
                Id = Id,
                Name = Name,
                LastName = LastName,
                CurrentJob = current,
                Contact = Contacts.ToDto()
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
            DOB = data.DOB ?? DOB;
            RFC = data.RFC ?? RFC;
            CURP = data.CURP ?? CURP;
            Username = data.Username ?? Username;
            data.Charges?.ForEach(Charges.UpdateObjectWithIndex<Job, NewJobDto, UpdatedJobDto>);
            data.ScheduleHistory?.ForEach(ScheduleHistory.UpdateObjectWithIndex<Schedule, NewScheduleDto, UpdatedScheduleDto>);
            data.Historial?.ForEach(Historial.UpdateObjectWithIndex<Record, NewRecordDto, UpdatedRecordDto>);
            if (data.Contacts is not null) Contacts.Update(data.Contacts);
        }

        private void GenerateKey()
        {
            InternalKey = "";
        }

        public Job? GetCurrentJob()
        {
            return Charges.Where(x => x.Active).SingleOrDefault();
        }
    }
}