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
        public DateTime Starts { get; private set; } = DateTime.Now;
        public DateTime Ends { get; private set; }
        public string PayHistory { get; private set; } = string.Empty;
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
            FTasksDto? last = Tasks
                .Where(x => !x.Complete)
                .MaxBy(x => x.Ends)?
                .ToDto();
            return new()
            {
                Id = Id,
                Name = Name,
                Starts = Starts,
                Ends = Ends,
                LastTask = last
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
            PayHistory = data.PayHistory ?? PayHistory;
            data.Tasks?.ForEach(Tasks.UpdateObjectWithIndex<FTasks, NewFTaskDto, UpdatedFTaskDto>);

            ModifiedAt = DateTime.Now;
        }
    }
}