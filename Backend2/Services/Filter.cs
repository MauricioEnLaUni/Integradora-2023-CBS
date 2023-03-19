using MongoDB.Driver;

namespace Fictichos.Constructora.Utilities.MongoDB
{
    public class Filter<T>
    {
        public readonly FilterDefinitionBuilder<T> filterBuilder =
            Builders<T>.Filter;
        public readonly ProjectionDefinitionBuilder<T> projectionBuilder =
            Builders<T>.Projection;
        public Dictionary<string, Action> Filters { get; } = new();
        public Dictionary<string, Action> Projects { get; } = new();
    }
}