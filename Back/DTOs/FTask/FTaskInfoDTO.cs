using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.DTOs
{
    public record FTaskInfoDTO
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Closed { get; set; }
        public List<FTasks> Subtasks { get; set; }
        public List<Employee> Assignees { get; set; }
        public List<Material> Material { get; set; }
        public Address? Address { get; set; }
    }
}