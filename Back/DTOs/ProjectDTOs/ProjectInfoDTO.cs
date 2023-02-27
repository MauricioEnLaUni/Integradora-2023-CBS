using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.DTOs
{
    public record ProjectInfoDTO
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Closed { get; set; }
        public Account? PayHistory { get; set; }
        public List<FTasks> Tasks { get; set; }
    }
}