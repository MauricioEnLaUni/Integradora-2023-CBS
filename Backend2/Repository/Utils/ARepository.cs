using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository
{
    public class RepositoryAsync<T, U> : IAsyncRepository<T>
    where T : Entity, IQueryMask<T, U>, new()
    {
        public readonly IMongoCollection<T> Collection;
        public readonly FilterDefinitionBuilder<T> filterBuilder =
            Builders<T>.Filter;

        public RepositoryAsync(MongoSettings container, string database, string col)
        {
            IMongoDatabase db = container.Client.GetDatabase(database);
            Collection = db.GetCollection<T>(col);
        }


        public async Task<T> CreateAsync(string data)
        {
            T newItem = new T().FakeConstructor(data);
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
            return await Collection.Find(new BsonDocument()).ToListAsync<T>();
        }

        public async Task<T?> GetByIdAsync(ObjectId id)
        {
            var filter = filterBuilder.Eq(e => e.Id, id);
            return await Collection.Find(filter).SingleOrDefaultAsync<T>();
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