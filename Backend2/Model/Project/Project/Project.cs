using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Dto;
using Fitichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Project : Entity
    {
        [BsonElement("account")]
        public Account? PayHistory { get; set; } = new("None", null);
        [BsonElement("tasks")]
        public List<FTasks> Tasks { get; set; } = new List<FTasks>();

        public Project(NewProjectDto newProject) : base(newProject.Name, null) { }

        public void Change(ProjectChangesDto chg)
        {
            if (chg.Name is not null) Name = chg.Name;
            if (chg.Tasks is not null) Tasks = chg.Tasks;
            if (Closed is not null) Closed = chg.Closed;
            if (PayHistory is not null) PayHistory = chg.PayHistory;
        }
    }
}