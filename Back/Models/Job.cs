using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
{
    public class Job : Entity
    {
        [BsonElement("salaryHistory")]
        public List<Salary> SalaryHistory { get; set; } = new List<Salary>();
        [BsonElement("role")]
        public string Role { get; set; }
        [BsonElement("area")]
        public string Area { get; set; }
        [BsonElement("responsibilities")]
        public List<string> Responsibilities { get;  set; } = new List<string>();

        public Job(string name, string role, string area) : base(name)
        {
            Role = role;
            Area = area;
        }

        public Job(NewJobDTO data) : base(data.Name)
        {
            Role = data.Role;
            Area = data.Area;
            Responsibilities = data.Responsibilities;
        }

        public JobInfoDTO AsDTO()
        {
            return new JobInfoDTO()
            {
                Name = this.Name,
                SalaryHistory = this.SalaryHistory.Select(s => s.AsDTO()).ToList(),
                Role = this.Role,
                Area = this.Area,
                Responsibilities = this.Responsibilities,
            };
        }


        public class Salary
        {
            public string Id { get; set; } = "";
            public DateTime Created { get; set; } = DateTime.Now;
            [BsonElement("reductions")]
            public Dictionary<string, double> Reductions { get; set; }
                = new Dictionary<string, double>();
            [BsonElement("rate")]
            public double Rate { get; set; }
            [BsonElement("hoursWeek")]
            public int? HoursWeek { get; set; }

            public Salary(double hourly)
            {
                Rate = hourly;
            }
            public Salary(double hourly, int hoursWeek)
            {
                Rate = hourly;
                HoursWeek = hoursWeek;
            }

            public SalaryInfoDTO AsDTO()
            {
                return new SalaryInfoDTO()
                {
                    Created = this.Created,
                    Reductions = this.Reductions,
                    Rate = this.Rate,
                    HoursWeek = this.HoursWeek
                };
            }
        }
    }
}