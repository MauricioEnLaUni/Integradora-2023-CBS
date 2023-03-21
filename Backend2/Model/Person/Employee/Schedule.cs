using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Schedule : BaseEntity,
        IQueryMask<Schedule, NewScheduleDto, UpdatedScheduleDto, ScheduleDto>
    {
        public string Period { get; set; } = string.Empty;
        public Dictionary<TimeSpan, int> Hours { get; set; } = new();
        public Address? Location { get; set; }

        public Schedule(NewScheduleDto data)
        {
            Period = data.Period;
            Hours = data.Hours;
            if (data.Location is not null)
            {
                Location = new Address().Instantiate(data.Location);
            }
        }
        public Schedule() { }
        public Schedule Instantiate(NewScheduleDto data)
        {
            return new(data);
        }
        public ScheduleDto ToDto()
        {
            AddressDto? loc = null;
            if (Location is not null) loc = Location.ToDto();
            return new()
            {
                Id = Id,
                Period = Period,
                Hours = Hours,
                Location = loc
            };
        }
        public string Serialize()
        {
            ScheduleDto data = ToDto();
            return  JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedScheduleDto data)
        {
            Period = data.Period ?? Period;
            Hours = data.Hours ?? Hours;
            
            if (data.Location is null) return;
            Location ??= new();
            Location.Update(data.Location);
        }
    }
}