using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Dto
{
    public record DtoBase
    {
        [BsonId]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
    }
    
    public record EntityDto : DtoBase
    {
        public string Name { get; set; } = string.Empty;
        public DateTime? Closed { get; set; }
    }
}