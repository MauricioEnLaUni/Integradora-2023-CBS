using MongoDB.Bson;
using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.DTOs
{
    public record UpdatedProjectDTO
    {
        public string id { get; set; }
        public string? Name { get; set; }
        public DateTime? Closed { get; set; }
        public Account? PayHistory { get; set; }
        public List<FTasks>? Tasks { get; set; }
    }
}