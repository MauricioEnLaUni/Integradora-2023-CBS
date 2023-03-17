using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
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