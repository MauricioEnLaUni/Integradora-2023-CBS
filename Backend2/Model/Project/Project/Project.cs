using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Project
        : BaseEntity, IQueryMask<Project, string, UpdatedProjectDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Responsible { get; private set; } = string.Empty;
        public DateTime Ends { get; private set; }
        public Account PayHistory { get; private set; } = new();
        public List<FTasks> Tasks { get; private set; } = new();

        public Project() { }
        public Project(string data)
        {
            Name = data;
        }
        public Project Instantiate(string data)
        {
            return new(data);
        }

        public ProjectDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name
            };
        }

        public string Serialize()
        {
            ProjectDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedProjectDto data)
        {
            Name = data.Name ?? Name;
            Ends = data.Ends ?? Ends;
            Responsible = data.Responsible ?? Responsible;
            data.Tasks?.ForEach(Tasks.UpdateObjectWithIndex<FTasks, NewFTaskDto, UpdatedFTaskDto>);
            if (data.PayHistory is not null) PayHistory.Update(data.PayHistory);
        }
    }
}