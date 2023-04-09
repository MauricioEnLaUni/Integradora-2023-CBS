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
        internal List<Salary> SalaryHistory { get; private set; } = new();
        internal string Name { get; private set; } = string.Empty;
        internal string Role { get; private set; } = string.Empty;
        internal string Area { get; private set; } = string.Empty;
        internal List<string> Oversees { get; private set; } = new();
        internal List<string> Leads { get; private set; } = new();
        internal List<string> Material { get; private set; } = new();
        internal string Parent { get; private set; } = string.Empty;
        internal List<string> Responsibilities { get;  private set; } = new();
        internal bool Active { get; private set; }
        internal List<string> Assignments { get; private set; } = new();

        public Job(NewJobDto data)
        {
            SalaryHistory.Add(new (data.SalaryHistory));
            Role = data.Role;
            Area = data.Area;
            Parent = data.Parent;
            Responsibilities = data.Responsibilities;
            Active = Active;
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
                Role = Role,
                Active = Active
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
            Parent = data.Parent ?? Parent;
            Active = data.Active;

            data.Oversees?.ForEach(Oversees.UpdateWithIndex);
            data.Responsibilities?.ForEach(Responsibilities.UpdateWithIndex);
            data.Material?.ForEach(Material.UpdateWithIndex);
        }
    }
}