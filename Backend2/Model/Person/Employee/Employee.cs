using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Employee : BaseEntity,
        IQueryMask<Employee, NewEmployeeDto, UpdatedEmployeeDto>
    {
        public bool Active { get; private set; } = false;
        public DateTime DOB { get; private set; }
        public string RFC { get; private set; } = string.Empty;
        public string CURP { get; private set; } = string.Empty;
        public List<byte[]> Documents { get; private set; } = new();
        public string InternalKey { get; private set; } = string.Empty;
        public List<Job> Charges { get; private set; } = new List<Job>();
        public List<Schedule> ScheduleHistory { get; private set; } = new List<Schedule>();
        public List<string> Oversees { get; private set; } = new();
        public List<Record> Historial { get; private set; } = new();
        public List<string> Assignments { get; set; } = new();

        public Employee() { }
        public Employee(NewEmployeeDto data)
        {
            DOB = data.DOB;
            CURP = data.CURP;
            RFC = data.RFC;
            Charges = new List<Job>
            {
                new Job(data.Charges)
            };
        }
        public Employee Instantiate(NewEmployeeDto data)
        {
            return new(data);
        }

        public EmployeeDto ToDto()
        {
            List<JobDto> JobList = new();
            List<ScheduleDto> ScheduleList = new();
            Charges.ForEach(e => {
                JobList.Add(e.ToDto());
            });
            ScheduleHistory.ForEach(e => {
                ScheduleList.Add(e.ToDto());
            });
            return new()
            {
                Id = Id,
                DOB = DOB,
                CURP = CURP,
                RFC = RFC,
            };
        }

        public string Serialize()
        {
            EmployeeDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedEmployeeDto data)
        {
            DOB = data.DOB ?? DOB;
            CURP = data.CURP ?? CURP;
            RFC = data.RFC ?? RFC;

            data.Assignments?.ForEach(Assignments.UpdateWithIndex);
            data.Oversees?.ForEach(Oversees.UpdateWithIndex);
            data.Charges?.ForEach(Charges.UpdateObjectWithIndex<Job, NewJobDto, UpdatedJobDto>);
            data.ScheduleHistory?.ForEach(ScheduleHistory.UpdateObjectWithIndex<Schedule, NewScheduleDto, UpdatedScheduleDto>);
        }
    }
}