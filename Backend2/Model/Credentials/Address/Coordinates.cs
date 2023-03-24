using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Model
{
    public class Coordinates
    {
        [BsonElement("latitude")]
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")]
        public string Latitude { get; set; } = string.Empty;
        [BsonElement("longitude")]
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")]
        public string Longitude { get; set; } = string.Empty;
    }
}