using MongoDB.Bson;

using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Model
{
    public class Education
    {
        [BsonElement("grades")]
        public List<Grade> Grades { get; set; } = new List<Grade>();
        [BsonElement("certifications")]
        public List<Grade> Certifications { get; set; } = new List<Grade>();
    }
}