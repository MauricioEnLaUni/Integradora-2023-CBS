using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    [BsonIgnoreExtraElements]
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private init; }
        [BsonElement("name")]
        public string Name { get; protected set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; init; }
        [BsonElement("deadline")]
        public DateTime? Closed { get; set; }

        public Entity(string name)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            CreatedAt = DateTime.Now;
        }

        public Entity(string name, DateTime? closes)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            if (closes is not null) Closed = closes;
        }
    }
}