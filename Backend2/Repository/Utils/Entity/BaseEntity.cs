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
        public string Id { get; init; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; init; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ModifiedAt { get; set; }

        public BaseEntity()
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }
    }
}