using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class FTasks : Entity, IQueryMask<FTasks, FTasksDto>
    {
        [BsonElement("starts")]
        public DateTime StartDate { get; set; }
        public new DateTime Closed { get; set; }
        [BsonElement("parent")]
        public ObjectId? Parent { get; set; }
        [BsonElement("subtasks")]
        public List<FTasks> Subtasks { get; set; } = new();
        [BsonElement("employees")]
        public List<ObjectId> EmployeesAssigned { get; set; } = new();
        [BsonElement("material")]
        public List<ObjectId> Material { get; set; } = new();
        [BsonElement("address")]
        public Address? Address { get; set; }

        public FTasks() { }
        public FTasks(NewFTaskDto newTask)
        {
            StartDate = newTask.StartDate;
            Closed = newTask.Closed;
            EmployeesAssigned = newTask.Assignees;
            if (newTask.Address is not null) Address = newTask.Address;
            if (newTask.Parent is not null) Parent = newTask.Parent;
        }

        public FTasks FakeConstructor(string dto)
        {
            try
            {
                return new FTasks(JsonConvert
                    .DeserializeObject<NewFTaskDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
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
                Closed = Closed!,
                Parent = Parent,
                Subtasks = list,
                EmployeesAssigned = EmployeesAssigned,
                Material = Material,
                Address = Address
            };
        }

        public string SerializeDto()
        {
            FTasksDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdateFTaskDto data)
        {
            if (data.Name is not null) Name = data.Name;
            if (data.StartDate is not null) StartDate = (DateTime)data.StartDate;
            data.Subtasks?.ForEach(UpdateSub);
            data.Material?.ForEach(e => {
                UpdateEmbedded(false, e);
            });
            data.EmployeesAssigned?.ForEach(e => {
                UpdateEmbedded(true, e);
            });
            if (data.Address is not null) Address = data.Address;
            if (data.Closed is not null) Closed = (DateTime)data.Closed!;
        }

        public void UpdateSub(UpdateSubtaskDto data)
        {
            switch(data.Operation)
            {
                case 0:
                    Subtasks.Add(new(data.NewTask!));
                    break;
                case 1:
                    Subtasks.RemoveAt(data.Key);
                    break;
                case 2:
                    Subtasks[data.Key].Update(data.Task!);
                    break;
                default:
                    throw new Exception("Empty data.");
            }
        }

        public void UpdateEmbedded(bool flag, UpdateEmbeddedDto data)
        {
            List<ObjectId> list = flag ? EmployeesAssigned : Material;
            switch(data.Operation)
            {
                case 0:
                    list.Add(data.Data);
                    break;
                case 1:
                    list.Remove(data.Data);
                    break;
                default:
                    break;
            }
        }
    }
}