using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Dto;
using Fitichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Employee : Entity
    {
        [BsonElement("active")]
        private bool Active { get; set; }
        [BsonElement("dob")]
        private DateOnly DOB { get; set; }
        [BsonElement]
        private string RFC { get; set; } = string.Empty;
        [BsonElement("curp")]
        private string CURP { get; set; }
        [BsonElement("charges")]
        private List<Job> Charges { get; set; } = new List<Job>();
        [BsonElement("scheduleHistory")]
        private List<Schedule> ScheduleHistory { get; set; } = new List<Schedule>();

        public Employee(NewEmployeeDto data) : base(data.Name, null)
        {
            DOB = data.DOB;
            CURP = data.CURP;
            RFC = data.RFC;
            Charges = new List<Job>();
            Charges.Add(new Job(data.Charges));
        }

        public class Schedule
        {
            ObjectId Id = ObjectId.GenerateNewId();
            [BsonElement("name")]
            public string Name { get; set; } = string.Empty;
            [BsonElement("hours")]
            public Dictionary<TimeSpan, int> Hours { get; set; } = new();
            [BsonElement("location")]
            public Address? Location { get; set; }

            public Schedule(NewScheduleDto data)
            {
                Name = data.Name;
                Hours = data.Hours;
                if (data.Location is not null)
                {
                    Location = new Address(data.Location);
                }
            }
        }

        public class Education
        {
            [BsonElement("grades")]
            public List<Grade> Grades { get; set; } = new List<Grade>();
            [BsonElement("certifications")]
            public List<Grade> Certification { get; set; } = new List<Grade>();

            public class Grade
            {
                [BsonElement("grade")]
                public Dictionary<string, string> SchoolGrade { get; set; } = new();
                [BsonElement("overseas")]
                public Dictionary<bool, string>? Overseas { get; set; }
                [BsonElement("period")]
                public TimeSpan Period { get; set; }

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