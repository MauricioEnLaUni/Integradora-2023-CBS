using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Repository
{
    /// <summary>
    /// Base Entity for Models
    /// </summary>
    /// <param name="Id">MongoDB Id, generated locally for new items.</param>
    /// <param name="CreatedAt">When it was inserted in the database.</param>
    /// <param name="ModifiedAt">Updates whenever the object does.</param>
    [BsonIgnoreExtraElements]
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; } = string.Empty;
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}