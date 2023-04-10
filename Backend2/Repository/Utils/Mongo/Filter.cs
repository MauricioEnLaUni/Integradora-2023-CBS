using MongoDB.Bson;
using MongoDB.Driver;

using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities.MongoDB
{
    public static class Filter
    {
        public static FilterDefinition<T>? GetFilter<T>(
            string choice, string field, BsonValue value)
        {
            FilterDefinitionBuilder<T> noun = Builders<T>.Filter;
            return choice switch
            {
                "eq" => noun.Eq(field, value),
                "gt" => noun.Gt(field, value),
                "gte" => noun.Gte(field, value),
                "lt" => noun.Lt(field, value),
                "lte" => noun.Lte(field, value),
                "ne" => noun.Ne(field, value),
                "in" => noun.In(field, (BsonArray)value),
                "nin" => noun.Nin(field, (BsonArray)value),
                "regex" => noun.Regex(field, (BsonRegularExpression)value),
                _ => noun.Where(_ => true),
            };
        }

        public static FilterDefinition<T> Empty<T>()
        {
            return Builders<T>.Filter.Where(_ => true);
        }

        public static FilterDefinition<T> ById<T>(string id)
            where T : BaseEntity
        {
            return Builders<T>.Filter.Eq(x => x.Id, id);
        }
    }
}