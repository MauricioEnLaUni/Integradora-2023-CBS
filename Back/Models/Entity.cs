using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    [BsonIgnoreExtraElements]
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string Id { get; init; }
        [BsonElement("name")]
        private string Name { get; set; }
        [BsonElement("createdAt")]
        private DateTime CreatedAt { get; set; }

        public Entity(string id, string name, DateTime created)
        {
            Id = id;
            Name = name;
            CreatedAt = created;
        }

        public Entity(string name)
        {
            Name = name;
            Id = "randomstring";
            CreatedAt = DateTime.Now;
        }

        public void Insert()
        {
            
        }
    }
}