using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Dto;
using Fitichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Job : Entity
    {
        [BsonElement("salaryHistory")]
        public List<Salary> SalaryHistory { get; set; } = new();
        [BsonElement("role")]
        public string Role { get; set; }
        [BsonElement("area")]
        public string Area { get; set; }
        [BsonElement("responsibilities")]
        public List<string> Responsibilities { get;  set; } = new();

        public Job(NewJobDto data) : base(data.Name, null)
        {
            Role = data.Role;
            Area = data.Area;
            Responsibilities = data.Responsibilities;
        }


        public class Salary
        {
            public string Id { get; set; } = "";
            public DateTime Created { get; set; } = DateTime.Now;
            [BsonElement("reductions")]
            public Dictionary<string, double> Reductions { get; set; } = new();
            [BsonElement("rate")]
            public double Rate { get; set; }
            [BsonElement("hoursWeek")]
            public int? HoursWeek { get; set; }
            
            public Salary(double hourly, int? hoursWeek)
            {
                Rate = hourly;
                if (hoursWeek is not null) HoursWeek = hoursWeek;
            }
        }
    }
}