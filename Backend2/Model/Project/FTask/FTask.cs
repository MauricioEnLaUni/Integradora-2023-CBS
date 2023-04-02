using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class FTasks : BaseEntity,
        IQueryMask<FTasks, NewFTaskDto, UpdatedFTaskDto>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime Ends { get; set; }
        public bool Complete { get; set; }
        public string Parent { get; } = string.Empty;
        public string Overseer { get; set; } = string.Empty;
        public List<FTasks> Subtasks { get; set; } = new();
        public List<string> EmployeesAssigned { get; set; } = new();
        public List<string> Material { get; set; } = new();
        public Address? Address { get; set; }
        public string Owner { get; set; } = string.Empty;

        public FTasks() { }
        public FTasks(NewFTaskDto newTask)
        {
            Name = newTask.Name;
            StartDate = newTask.StartDate;
            Ends = newTask.Ends;
            Parent = newTask.Parent;
            EmployeesAssigned = newTask.Assignees;
            Overseer = newTask.Overseer;
            Owner = newTask.Owner;
            if (newTask.Address is not null) Address = newTask.Address;
            if (newTask.Parent is not null) Parent = newTask.Parent;
        }

        public FTasks Instantiate(NewFTaskDto data)
        {
            return new(data);
        }

        public FTasksDto ToDto()
        {
            List<FTasksDto>? list = new();
            if (Subtasks.Any())
            {
                Subtasks.ForEach(e => {
                    list.Add(e.ToDto());
                });
            }
            return new()
            {
                Id = Id,
                StartDate = StartDate,
                Ends = Ends,
                Subtasks = list,
                EmployeesAssigned = EmployeesAssigned,
                Material = Material,
                Address = Address
            };
        }

        public string Serialize()
        {
            FTasksDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedFTaskDto data)
        {
            Name = data.Name ?? Name;
            StartDate = data.StartDate ?? StartDate;
            Address = data.Address ?? Address;
            Ends = data.Ends ?? Ends;

            data.Material?.ForEach(Material.UpdateWithIndex);
            data.EmployeesAssigned?.ForEach(EmployeesAssigned.UpdateWithIndex);
            data.Subtasks?.ForEach(Subtasks.UpdateObjectWithIndex<FTasks, NewFTaskDto, UpdatedFTaskDto>);
        }
    }
}