using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Employee : Entity
    {
        [BsonElement("active")]
        private bool Active { get; set; }
        [BsonElement("dob")]
        private DateTime DOB { get; set; }
        [BsonElement("curp")]
        private string CURP { get; set; }
        [BsonElement("charges")]
        private List<Job> Charges { get; set; } = new List<Job>();
        [BsonElement("scheduleHistory")]
        private List<Schedule> ScheduleHistory { get; set; } = new List<Schedule>();

        public Employee(string name, bool state, DateTime dob, string curp) : base(name)
        {
            Active = state;
            DOB = dob;
            CURP = curp;
        }

        private class Schedule : Entity
        {
            [BsonElement("hours")]
            private List<TimeSpan> Hours { get; set; } = new List<TimeSpan>();
            [BsonElement("location")]
            private Address? Location { get; set; }

            public Schedule(string name) : base(name) { }
            public Schedule(string name, DateTime closes) : base(name, closes) { }
            public Schedule(string name, string[] address) : base(name)
            {
                Location = new Address(address);
            }
            public Schedule(string name, string[] address, DateTime closes) : base(name, closes)
            {
                Location = new Address(address);
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