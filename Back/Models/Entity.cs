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
        protected string Name { get; set; }
        [BsonElement("createdAt")]
        protected DateTime CreatedAt { get; init; }
        [BsonElement("deadline")]
        protected DateTime? Closed { get; set; }

        public Entity(string name)
        {
            Id = CreateId();
            Name = name;
            CreatedAt = DateTime.Now;
        }

        public Entity(string name, DateTime created)
        {
            Id = CreateId();
            Name = name;
            CreatedAt = created;
        }

        public Entity(string name, DateTime created, DateTime closed)
        {
            Id = CreateId();
            Name = name;
            CreatedAt = created;
            Closed = closed;
        }

        private static string CreateId() {
            return "0";
        }
    }
}