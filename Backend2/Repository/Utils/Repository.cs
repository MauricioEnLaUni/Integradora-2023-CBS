using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fitichos.Constructora.Repository
{
    public class Repository<T, U> : IRepository<T, U>
    where T : Entity
    {
        private string databaseName = "cbs";
        private string collectionName = "projects";
        public IMongoCollection<T> _col;

        public Repository(IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(databaseName);
            _col = db.GetCollection<T>(collectionName);
        }

        public async Task CreateAsync(T newItem)
        {
            await _col.InsertOneAsync(newItem);
        }

        public async Task DeleteAsync(string Id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(Id));
            await _col.DeleteOneAsync(filter);
        }

        public List<T> GetAll()
        {
            return 
                (from e in _col.AsQueryable<T>()
                select e).ToList<T>();
        }

        public T? GetById(string id)
        {
            ObjectId _id = new(id);
            return
                (from e in _col.AsQueryable<T>()
                where e.Id == _id
                select e).SingleOrDefault<T>();
        }

        public async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(item => item.Id, entity.Id);
            await _col.ReplaceOneAsync(filter, entity);
        }
    }
}