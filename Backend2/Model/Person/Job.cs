using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class Job : BaseEntity, IQueryMask<Job, NewJobDto, UpdatedJobDto>
    {
        public string Name { get; set; } = string.Empty;
        public List<Salary> SalaryHistory { get; set; } = new();
        public string Role { get; set; } = string.Empty;
        public ObjectId Area { get; set; }
        public ObjectId Responsible { get; set; }
        public List<ObjectId> Material { get; set; } = new();
        public ObjectId Parent { get; set; }
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
        public string Serialize()
        {
            JobDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public Job Instantiate(NewJobDto data)
        {
            return new(data);
        }

        public void Update(UpdatedJobDto data)
        {

        }
    }
}