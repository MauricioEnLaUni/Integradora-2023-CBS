using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fitichos.Constructora.Repository
{
    [BsonIgnoreExtraElements]
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("deadline")]
        public DateTime? Closed { get; set; }

        public Entity(string name, DateTime? closes)
        {
            Name = name;
            CreatedAt = DateTime.Now;
            if (closes is not null) Closed = closes;
        }
    }
}