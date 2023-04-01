using MongoDB.Bson;
using MongoDB.Driver;

namespace Fictichos.Constructora.Utilities.MongoDB
{
    public static class Filter
    {
        public static FilterDefinition<T>? GetFilter<T>(
            string choice, string field, BsonValue value)
        {
            FilterDefinitionBuilder<T> noun = Builders<T>.Filter;
            switch(choice)
            {
                case "eq":
                    return noun.Eq(field, value);
                case "gt":
                    return noun.Gt(field, value);
                case "gte":
                    return noun.Gte(field, value);
                case "lt":
                    return noun.Lt(field, value);
                case "lte":
                    return noun.Lte(field, value);
                case "ne":
                    return noun.Ne(field, value);
                case "in":
                    return noun.In(field, (BsonArray)value);
                case "nin":
                    return noun.Nin(field, (BsonArray)value);
                case "regex":
                    return noun.Regex(field, (BsonRegularExpression)value);
                default:
                    return noun.Where(_ => true);
            }
        }

        public static FilterDefinition<T> EmptyFilter<T>()
        {
            return Builders<T>.Filter.Where(_ => true);
        }
    }
}