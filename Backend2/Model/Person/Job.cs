using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class Job : Entity, IQueryMask<Job, JobDto, UpdatedJobDto>
    {
        [BsonElement("salaryHistory")]
        public List<Salary> SalaryHistory { get; set; } = new();
        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;
        [BsonElement("area")]
        public ObjectId Area { get; set; }
        [BsonElement("responsible")]// Jefe inmediato
        public ObjectId Responsible { get; set; }
        [BsonElement("material")]
        public List<ObjectId> Material { get; set; } = new();
        [BsonElement("parent")]
        public ObjectId Parent { get; set; }
        [BsonElement("responsibilities")]
        public List<string> Responsibilities { get;  set; } = new();

        public Job(NewJobDto data)
        {
            Role = data.Role;
            Area = data.Area;
            Responsibilities = data.Responsibilities;
        }
        public Job() { }
        
        public JobDto ToDto()
        {
            List<SalaryDto> list = new();
            SalaryHistory.ForEach(e => {
                list.Add(e.ToDto());
            });
            return new()
            {
                Id = Id,
                Name = Name,
                SalaryHistory = list,
                Role = Role
            };
        }
        public string SerializeDto()
        {
            JobDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public Job FakeConstructor(string dto)
        {
            try
            {
                return new Job(JsonConvert
                    .DeserializeObject<NewJobDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public void Update(UpdatedJobDto data)
        {

        }
    }
}