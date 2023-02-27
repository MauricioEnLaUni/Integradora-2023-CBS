using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
{
    public class Employee : Entity
    {
        [BsonElement("active")]
        private bool Active { get; set; }
        [BsonElement("dob")]
        private DateOnly DOB { get; set; }
        [BsonElement("curp")]
        private string CURP { get; set; }
        [BsonElement("charges")]
        private List<Job> Charges { get; set; } = new List<Job>();
        [BsonElement("scheduleHistory")]
        private List<Schedule> ScheduleHistory { get; set; } = new List<Schedule>();

        public Employee(string name, bool state, DateOnly dob, string curp) : base(name)
        {
            Active = state;
            DOB = dob;
            CURP = curp;
        }

        public Employee(NewEmployeeDTO data) : base(data.Name)
        {
            DOB = data.DOB;
            CURP = data.CURP;
            Charges = new List<Job>();
            Charges.Add(new Job(data.Charges));
        }

        public EmployeeInfoDTO AsDTO()
        {
            return new EmployeeInfoDTO()
            {
                Name = this.Name,
                CreatedAt = this.CreatedAt,
                DOB = this.DOB,
                Charges = this.Charges.Select(c => c.AsDTO()).ToList(),
                ScheduleHistory = this.ScheduleHistory.Select(s => s.AsDTO()).ToList()
            };
        }

        private class Schedule : Entity
        {
            [BsonElement("hours")]
            private List<TimeSpan> Hours { get; set; } = new List<TimeSpan>();
            [BsonElement("location")]
            private Address? Location { get; set; }

            public Schedule(string name, DateTime? created) : base(name, created) { }
            public Schedule(string name, DateTime? created, string[] address) : base(name, created)
            {
                Location = new Address(address);
            }

            public Schedule(ScheduleInfoDTO data) : base(data.Name)
            {
                if (data.Location is not null)
                {
                    Location = new Address(data.Location);
                }
            }

            public ScheduleInfoDTO AsDTO()
            {
                return new ScheduleInfoDTO()
                {
                    Name = this.Name,
                    Hours = this.Hours,
                    Location = this.Location is not null ? this.Location.AsDTO() : null
                };
            }
        }

        private class Education
        {
            [BsonElement("grades")]
            private List<Grade> Grades { get; set; } = new List<Grade>();
            [BsonElement("certifications")]
            private List<Grade> Certification { get; set; } = new List<Grade>();

            private class Grade
            {
                [BsonElement("grade")]
                Dictionary<string, string> SchoolGrade { get; set; }
                    = new Dictionary<string, string>();
                [BsonElement("overseas")]
                private Dictionary<bool, string>? Overseas { get; set; }
                [BsonElement("period")]
                private TimeSpan Period { get; set; }

                public Grade(DateTime start, DateTime end)
                {
                    Period = end.Subtract(start);
                }

                public Grade(DateTime start, DateTime end, bool overseas, string equivalent)
                {
                    Period = end.Subtract(start);
                    Overseas = new Dictionary<bool, string>
                    {
                        { overseas, equivalent }
                    };
                }
            }
        }
    }
}