using System.Linq;
using System.Reflection;

using AutoMapper;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.Database;
using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.Repository
{
    public class MongoDBRepository<T, U, V>
        where T : ITBase, IUpdater<V>
        where V : DTOBase
    {
        private Connector<T> _conn = new(0, "people");
        private readonly IMapper _mapper;

        public MongoDBRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T ParseDTO(U newDTO)
        {
            T record = _mapper.Map<T>(newDTO);
            return record;
        }
        public U ToDTO(T record)
        {
            U DTO = _mapper.Map<U>(record);
            return DTO;
        }

        public void Create(U newDTO)
        {
            T record = ParseDTO(newDTO);
            _conn.Collection.InsertOne(record);
        }

        public IMongoQueryable<T> ReadAll()
        {
            return
                from x in _conn.Collection.AsQueryable()
                select x;
        }

        public T? ReadById(string id)
        {
            ObjectId _id = new(id);
            return
                (from x in _conn.Collection.AsQueryable()
                where x.Id == _id
                select x).SingleOrDefault();
        }

        public bool Update(V update)
        {
            var result = typeof(T).GetProperties()
                .Select(x => new { property = x.Name, value = x.GetValue(update) })
                .Where(x => x.value is not null)
                .ToList();
            if (!result.Any()) return false;
            
            Type type = result.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());
            
            var filter = Builders<T>.Filter.Eq("Id", new ObjectId(update.Id));
            var newData = Builders<T>.Update
                .Set(x => )
            return true;
        }

        public async Task<DeleteResult> Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", new ObjectId(id));
            return await _conn.Collection.DeleteOneAsync(filter);
        }
    }
}