using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Repository
{
    /// <summary>
    /// Base Entity for Models
    /// </summary>
    /// <param name="Id">MongoDB Id, generated locally for new items.</param>
    /// <param name="CreatedAt"></param>
    [BsonIgnoreExtraElements]
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; } = ObjectId.GenerateNewId();
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime ModifiedAt { get; } = DateTime.Now;
    }
}