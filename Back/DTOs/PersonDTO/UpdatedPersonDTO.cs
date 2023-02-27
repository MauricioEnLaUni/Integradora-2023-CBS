using MongoDB.Bson;
using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.DTOs
{
    public record UpdatedPersonDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set;}
        public Contact? Contacts { get; set; }
        public EmployeeInfoDTO? Employed { get; set; }
    }
}