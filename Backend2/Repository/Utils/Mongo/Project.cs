using MongoDB.Bson;
using MongoDB.Driver;

namespace Fictichos.Constructora.Utilities.MongoDB
{
    public class Projection
    {
        public ProjectionDefinition<T, U> Project<T, U>(ProjectionDto data)
        {
            if (data.Flag) return Builders<T>.Projection.Include(data.Field);
            return Builders<T>.Projection.Exclude(data.Field);
        }
    }
}