using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Repository
{
    [BsonIgnoreExtraElements]
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private ObjectId Id { get; } = ObjectId.GenerateNewId();
        [BsonElement("createdAt")]
        private DateTime CreatedAt { get; } = DateTime.Now;
    }
}