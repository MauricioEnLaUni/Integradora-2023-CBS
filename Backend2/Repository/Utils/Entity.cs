using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository
{
    [BsonIgnoreExtraElements]
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } = new ObjectId();
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonElement("deadline")]
        public DateTime? Closed { get; set; }

        public Entity() { }

        protected void SetFields(EntityDto data)
        {
            Name = data.Name;
            Closed = data.Closed ?? null;
        }
    }
}