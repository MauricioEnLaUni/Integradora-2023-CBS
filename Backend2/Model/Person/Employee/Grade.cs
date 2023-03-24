using MongoDB.Bson;

using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Model
{
    public class Grade
    {
        [BsonElement("grade")]
        public Dictionary<string, string> SchoolGrade { get; set; } = new();
        [BsonElement("overseas")]
        public Dictionary<bool, string>? Overseas { get; set; }

        public Grade(DateTime start, DateTime end, bool overseas, string equivalent)
        {
            Overseas = new Dictionary<bool, string>
            {
                { overseas, equivalent }
            };
        }
    }
}