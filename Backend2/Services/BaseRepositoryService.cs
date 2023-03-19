using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository
{
    public abstract class BaseRepositoryService<T, U, V>
        : IRepositoryService<T, U, V>
        where T : AbstractEntity<T, U, V>, new()
    {
        public abstract Dictionary<string, Action<T, dynamic>>
            Properties { get; }
        public Dictionary<string,
            Func<string, BsonValue, FilterDefinition<T>>> Filters { get; init; }

        public readonly IMongoCollection<T> Collection;
        public readonly FilterDefinitionBuilder<T> filterBuilder =
            Builders<T>.Filter;
        public readonly ProjectionDefinitionBuilder<T> projectBuilder =
            Builders<T>.Projection;

        public BaseRepositoryService(
            MongoSettings container,
            string DB,
            string COL)
        {
            Filters = new Filter<T>().Filters;
            IMongoDatabase db = container.Client.GetDatabase(DB);
            Collection = db.GetCollection<T>(COL);
        }

        public async Task<T> CreateAsync(string data)
        {
            T newItem = new T().Instantiate(data);
            await Collection.InsertOneAsync(newItem);
            return newItem;
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Collection.Find(new BsonDocument())
                .ToListAsync<T>();
        }

        public async Task<T?> GetByIdAsync(ObjectId id)
        {
            var filter = filterBuilder.Eq(e => e.Id, id);
            return await Collection.Find(filter)
                .SingleOrDefaultAsync<T>();
        }

        public async Task UpdateAsync(T data)
        {
            var filter = filterBuilder.Eq(item => item.Id, data.Id);
            await Collection.ReplaceOneAsync(filter, data);
        }

        public async Task<List<T>> GetListByFilterAsync(
            FilterDefinition<T> filter)
        {
            return await Collection.Find(filter).ToListAsync<T>();
        }

        public async Task<T?> GetOneByFilterAsync(FilterDefinition<T> filter)
        {
            return await Collection.Find(filter).SingleOrDefaultAsync<T>();
        }
    }
}