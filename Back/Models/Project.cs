using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    [BsonIgnoreExtraElements]
    public record Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        [BsonElement("name")]
        private string Name { get; init; }
        [BsonElement("createdAt")]
        private DateTime CreatedAt { get; init; }

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
    }
}