using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Job : Entity
    {
        [BsonElement("salaryHistory")]
        private List<Salary> SalaryHistory { get; set; } = new List<Salary>();
        [BsonElement("role")]
        private string Role { get; set; }
        [BsonElement("area")]
        private string Area { get; set; }
        [BsonElement("responsibilities")]
        public List<string> Responsibilities { get; private set; } = new List<string>();

        public Job(string name, string role, string area) : base(name)
        {
            Role = role;
            Area = area;
        }


        private class Salary
        {
            private string Id { get; set; } = "";
            private DateTime Created { get; set; } = DateTime.Now;
            private DateTime? Closed { get; set; }
            [BsonElement("reductions")]
            private Dictionary<string, double> Reductions { get; set; }
                = new Dictionary<string, double>();
            [BsonElement("rate")]
            private double Rate { get; set; }
            [BsonElement("hoursWeek")]
            private int? HoursWeek { get; set; }

            public Salary(double hourly)
            {
                Rate = hourly;
            }

            public Salary(double hourly, DateTime closes)
            {
                Rate = hourly;
                Closed = closes;
            }
            public Salary(double hourly, DateTime closes, int hoursWeek)
            {
                Rate = hourly;
                Closed = closes;
                HoursWeek = hoursWeek;
            }
        }
    }
}