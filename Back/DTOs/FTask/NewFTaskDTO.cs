using MongoDB.Bson;
using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.DTOs
{
    public record NewFTaskDTO
    {
        public ObjectId Owner { get; set; }
        public string Name { get; set; }
        public DateTime? Closed { get; set; }
        public List<Employee> Assignees { get; set; }
        public Address? Address { get; set; }
    }
}