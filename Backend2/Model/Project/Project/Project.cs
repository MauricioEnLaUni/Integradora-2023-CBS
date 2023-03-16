using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Project
        : Entity, IQueryMask<Project, ProjectDto, UpdatedProjectDto>
    {
        [BsonElement("responsible")]
        public ObjectId Responsible { get; private set; }
        [BsonElement("account")]
        public Account? PayHistory { get; private set; }
        [BsonElement("tasks")]
        public List<FTasks> Tasks { get; private set; } = new();

        public Project() { }
        public Project(NewProjectDto data)
        {
            Name = data.Name;
        }
        public Project FakeConstructor(string dto)
        {
            try
            {
                return new Project(JsonConvert
                    .DeserializeObject<NewProjectDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public ProjectDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name
            };
        }

        public string SerializeDto()
        {
            ProjectDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }


        public void Update(UpdatedProjectDto data)
        {
            if (data.Name is not null) Name = data.Name;
            if (data.Tasks is not null) Tasks = data.Tasks;
            if (Closed is not null) Closed = data.Closed;
            if (PayHistory is not null) PayHistory = data.PayHistory;
        }
    }
}