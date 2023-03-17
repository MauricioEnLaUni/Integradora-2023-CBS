using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Schedule : Entity,
        IQueryMask<Schedule, ScheduleDto, UpdatedScheduleDto>
    {
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
                Location = new Address().FakeConstructor(data.Location);
            }
        }
        public Schedule() { }
        public Schedule FakeConstructor(string dto)
        {
            try
            {
                return new Schedule(JsonConvert
                    .DeserializeObject<NewScheduleDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }
        public ScheduleDto ToDto()
        {
            AddressDto? loc = null;
            if (Location is not null) loc = Location.ToDto();
            return new()
            {
                Id = Id,
                Name = Name,
                Hours = Hours,
                Location = loc
            };
        }
        public string SerializeDto()
        {
            ScheduleDto data = ToDto();
            return  JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedScheduleDto data)
        {
            Name = data.Name ?? Name;
            if (data.Location is not null)
            {

                Location ??= new();
                Location.Update(data.Location);
            }
            Hours = data.Hours ?? Hours;
        }
    }
}