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
        private DateTime CreatedAt { get; init; }
        [BsonElement("deadline")]
        protected DateTime? Closed { get; set; }

        public Entity(string name)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            CreatedAt = DateTime.Now;
        }

        public Entity(string name, DateTime created)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            CreatedAt = created;
        }

        public Entity(string name, DateTime created, DateTime closed)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            CreatedAt = created;
            Closed = closed;
        }
    }
}