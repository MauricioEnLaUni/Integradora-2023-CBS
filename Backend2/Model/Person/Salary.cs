using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class Salary : BaseEntity,
        IQueryMask<Salary, NewSalaryDto, UpdatedSalaryDto>
    {
        public string Period { get; set; } = string.Empty;
        public string Due { get; set; } = string.Empty;
        public Dictionary<string, double> Reductions { get; set; } = new();
        public double HourlyRate { get; set; }
        public int? HoursWeeklyCap { get; set; }
        public DateTime? Closed { get; set; }

        public Salary() { }
        
        public Salary(NewSalaryDto data)
        {
            Period = data.Period;
            Due = data.Due;
            Reductions = data.Reductions;
            HourlyRate = data.HourlyRate;
            HoursWeeklyCap = data.HoursWeeklyCap ?? null;
        }

        public Salary Instantiate(NewSalaryDto data)
        {
            return new(data);
        }
        public SalaryDto ToDto()
        {
            return new()
            {
                Id = Id,
                Period = Period,
                Due = Due,
                Reductions = Reductions,
                HourlyRate = HourlyRate,
                HoursWeeklyCap = HoursWeeklyCap
            };
        }

        public string Serialize()
        {
            SalaryDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedSalaryDto data)
        {
            Period = data.Period ?? Period;
            Due = data.Due ?? Due;
            Reductions = data.Reductions ?? Reductions;
            HoursWeeklyCap = data.HoursWeeklyCap ?? HoursWeeklyCap;
            HourlyRate = data.HourlyRate ?? HourlyRate;
            Closed = data.Closed ?? Closed;
        }
    }
}