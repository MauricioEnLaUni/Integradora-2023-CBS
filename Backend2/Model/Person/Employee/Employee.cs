using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Employee : Entity,
        IQueryMask<Employee, EmployeeDto, UpdatedEmployeeDto>
    {
        [BsonElement("active")]
        public bool Active { get; private set; } = false;
        [BsonElement("dob")]
        public DateOnly DOB { get; private set; }
        [BsonElement]
        public string RFC { get; private set; } = string.Empty;
        [BsonElement("curp")]
        public string CURP { get; private set; } = string.Empty;
        public List<byte[]> Documents { get; private set; } = new();
        // visa pass w.e.
        // INE
        // Fotos de documentos => se vera
        // expediente > sanciones - bonos - permisos - faltas
        [BsonElement("internal")]
        public string InternalKey { get; private set; } = string.Empty;
        [BsonElement("charges")]
        public List<Job> Charges { get; private set; } = new List<Job>();
        [BsonElement("scheduleHistory")]
        public List<Schedule> ScheduleHistory { get; private set; } = new List<Schedule>();

        public Employee() { }
        public Employee(NewEmployeeDto data)
        {
            Name = data.Name;
            DOB = data.DOB;
            CURP = data.CURP;
            RFC = data.RFC;
            Charges = new List<Job>
            {
                new Job(data.Charges)
            };
        }
        public Employee FakeConstructor(string dto)
        {
            try
            {
                return new Employee(JsonConvert
                    .DeserializeObject<NewEmployeeDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
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
                Name = Name,
                DOB = DOB,
                CURP = CURP,
                RFC = RFC,
            };
        }

        public string SerializeDto()
        {
            EmployeeDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedEmployeeDto data)
        {
            Name = data.Name ?? Name;
            DOB = data.DOB ?? DOB;
            CURP = data.CURP ?? CURP;
            RFC = data.RFC ?? RFC;
            data.Charges?.ForEach(UpdateChargesList);
            data.ScheduleHistory?.ForEach(UpdateScheduleList);
        }

        public void UpdateChargesList(ListUpdatedChargesDto data)
        {
            if (data is null) return;
            switch(data.Operation)
            {
                case 0 :
                    Charges.Add(new (data.NewData!));
                    break;
                case 1:
                    Charges.RemoveAt(data.Key);
                    break;
                case 2:
                    Charges[data.Key].Update(data.UpdatedData!);
                    break;
                default:
                    break;
            }
        }

        public void UpdateScheduleList(ListUpdatedScheduleDto data)
        {
            if (data is null) return;
            switch(data.Operation)
            {
                case 0 :
                    ScheduleHistory.Add(new (data.NewData!));
                    break;
                case 1:
                    ScheduleHistory.RemoveAt(data.Key);
                    break;
                case 2:
                    ScheduleHistory[data.Key].Update(data.UpdatedData!);
                    break;
                default:
                    break;
            }
        }
    }
}