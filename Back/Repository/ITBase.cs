using MongoDB.Bson;

namespace Fictichos.Constructora.Models
{
    public class ITBase
    {
        public ObjectId Id { get; private set; }
        public string Name { get; set; }
    }
}