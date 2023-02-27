using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.DTOs;

namespace Fictichos.Constructora.Models
{
    public class Project : Entity
    {
        [BsonElement("account")]
        public Account? PayHistory { get; set; }
        [BsonElement("tasks")]
        public List<FTasks> Tasks { get; } = new List<FTasks>();

        public Project(string name, string account) : base(name)
        {
            PayHistory = new Account(account);
        }

        public Project(NewProjectDTO newProject) : base(newProject.Name) { }

        public ProjectInfoDTO AsDTO()
        {
            return new ProjectInfoDTO
            {
                Name = this.Name,
                CreatedAt = this.CreatedAt,
                Tasks = this.Tasks,
                Closed = this.Closed,
                PayHistory = this.PayHistory
            };
        }
    }
}