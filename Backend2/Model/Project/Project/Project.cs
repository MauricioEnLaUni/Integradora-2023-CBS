using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Project : Entity, IQueryMask<Project>
    {
        [BsonElement("responsible")]
        public ObjectId Responsible { get; private set; }
        [BsonElement("account")]
        public Account? PayHistory { get; private set; }
        [BsonElement("tasks")]
        public List<FTasks> Tasks { get; private set; } = new();

        public Project() { }
        private Project(NewProjectDto data)
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
        public string AsDto()
        {
            return JsonConvert.SerializeObject(this);
        }


        public void Change(ProjectChangesDto chg)
        {
            if (chg.Name is not null) Name = chg.Name;
            if (chg.Tasks is not null) Tasks = chg.Tasks;
            if (Closed is not null) Closed = chg.Closed;
            if (PayHistory is not null) PayHistory = chg.PayHistory;
        }
    }
}