using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class Job : BaseEntity, IQueryMask<Job, NewJobDto, UpdatedJobDto, JobDto>
    {
        public List<Salary> SalaryHistory { get; set; } = new();
        public string Role { get; set; } = string.Empty;
        [BsonRepresentation(BsonType.ObjectId)]
        public string Area { get; set; } = string.Empty;
        [BsonRepresentation(BsonType.ObjectId)]
        public string Responsible { get; set; } = string.Empty;
        public List<string> Material { get; set; } = new();
        [BsonRepresentation(BsonType.ObjectId)]
        public string Parent { get; set; } = string.Empty;
        public List<string> Responsibilities { get;  set; } = new();

        public Job(NewJobDto data)
        {
            SalaryHistory.Add(new (data.SalaryHistory));
            Role = data.Role;
            Area = data.Area;
            Parent = Parent;
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
            Role = data.Role ?? Role;
            Area = data.Area ?? Area;
            Responsible = data.Responsible ?? Responsible;
            Parent = data.Parent ?? Parent;
            data.Responsibilities?.ForEach(Responsibilities.UpdateWithIndex);
            data.Material?.ForEach(Material.UpdateWithIndex);
        }
    }
}