using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;

namespace Fictichos.Constructora.Dto
{
    public record DtoBase
    {
        [Required]
        public ObjectId Id { get; set; }
    }
    
    public record EntityDto : DtoBase
    {
        public string Name { get; set; } = string.Empty;
        public DateTime? Closed { get; set; }
    }
}