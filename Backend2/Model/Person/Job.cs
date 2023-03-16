using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Newtonsoft.Json;

namespace Fictichos.Constructora.Model
{
    public class Job : Entity, IQueryMask<Job, JobDto, UpdatedJobDto>
    {
        [BsonElement("salaryHistory")]
        public List<Salary> SalaryHistory { get; set; } = new();
        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;
        [BsonElement("area")]
        public string Area { get; set; } = string.Empty;
        [BsonElement("responsible")]// Jefe inmediato
        public ObjectId Responsible { get; set; }
        [BsonElement("material")]
        public List<ObjectId> Material { get; set; } = new();
        [BsonElement("parent")]
        public ObjectId Parent { get; set; }
        [BsonElement("responsibilities")]
        public List<string> Responsibilities { get;  set; } = new();

        public Job(NewJobDto data)
        {
            Role = data.Role;
            Area = data.Area;
            Responsibilities = data.Responsibilities;
        }
        public Job() { }
        
        public JobDto ToDto()
        {
            List<SalaryDto> list = new();
            SalaryHistory.ForEach(e => {
                list.Add(e.ToDto());
            });
            return new()
            {
                Id = Id,
                Name = Name,
                SalaryHistory = list,
                Role = Role
            };
        }
        public string SerializeDto()
        {
            JobDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public Job FakeConstructor(string dto)
        {
            try
            {
                return new Job(JsonConvert
                    .DeserializeObject<NewJobDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public void Update(UpdatedJobDto data)
        {

        }

        public class Salary : Entity,
            IQueryMask<Salary, SalaryDto, UpdatedSalaryDto>
        {
            [BsonElement("reductions")]
            public Dictionary<string, double> Reductions { get; set; } = new();
            [BsonElement("rate")]
            public double Rate { get; set; }
            [BsonElement("payPeriod")]
            public int PayPeriod { get; set; } 
            [BsonElement("hoursWeek")]
            public int? HoursWeek { get; set; }

            public Salary() { }
            
            public Salary(NewSalaryDto data)
            {
                Reductions = data.Reductions;
                Rate = data.Rate;
                PayPeriod = data.PayPeriod;
                HoursWeek = data.HoursWeek ?? null;
            }

            public Salary FakeConstructor(string dto)
            {
                try
                {
                    return new Salary(JsonConvert
                        .DeserializeObject<NewSalaryDto>(dto, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Error
                    })!);
                }
                catch
                {
                    throw new JsonSerializationException();
                }
            }
            public SalaryDto ToDto()
            {
                return new()
                {
                    Id = Id,
                    Reductions = Reductions,
                    Rate = Rate,
                    HoursWeek = HoursWeek
                };
            }

            public string SerializeDto()
            {
                SalaryDto data = ToDto();
                return JsonConvert.SerializeObject(data);
            }

            public void Update(UpdatedSalaryDto data)
            {
                Closed = data.Closed ?? Closed;
                Reductions = data.Reductions ?? Reductions;
                PayPeriod = data.PayPeriod ?? PayPeriod;
                HoursWeek = data.HoursWeek ?? HoursWeek;
                Rate = data.Rate ?? Rate;
            }
        }
    }
}