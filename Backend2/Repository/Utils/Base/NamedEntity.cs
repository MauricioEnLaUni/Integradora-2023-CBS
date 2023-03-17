using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Repository
{
    [BsonIgnoreExtraElements]
    public class NamedEntity : BaseEntity
    {
        [BsonElement("createdAt")]
        public string Name { get; set; } = string.Empty;
    }
}