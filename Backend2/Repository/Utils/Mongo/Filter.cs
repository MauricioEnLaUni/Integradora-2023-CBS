using MongoDB.Bson;
using MongoDB.Driver;

namespace Fictichos.Constructora.Utilities.MongoDB
{
    public class Filter<T>
    {
        public Dictionary<string,
            Func<string, BsonValue, FilterDefinition<T>>>
                Filters { get; } = new()
        {
            { "eq", (field, value) => Builders<T>.Filter.Eq(field, value) },
            { "gt", (field, value) => Builders<T>.Filter.Gt(field, value) },
            { "gte", (field, value) => Builders<T>.Filter.Gte(field, value) },
            { "lt", (field, value) => Builders<T>.Filter.Lt(field, value) },
            { "lte", (field, value) => Builders<T>.Filter.Lte(field, value) },
            { "ne", (field, value) => Builders<T>.Filter.Ne(field, value) },
            { "in", (field, value)
                => Builders<T>.Filter.In(field, (BsonArray)value) },
            { "nin", (field, value)
                => Builders<T>.Filter.Nin(field, (BsonArray)value) },
            { "regex", (field, value)
                    => Builders<T>.Filter
                        .Regex(field, (BsonRegularExpression)value) }
        };
    }
}