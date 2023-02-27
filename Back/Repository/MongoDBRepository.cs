using AutoMapper;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.Database;
using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.Repository
{
    public class MongoDBRepository<T, U, V> where T : ITBase
    {
        private Connector<T> _conn = new Connector<T>(0, "people");
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
                select x).SingleOrDefault<T>();
        }

        public void Update(V update) { }
        public async Task<DeleteResult> Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", new ObjectId(id));
            return await _conn.Collection.DeleteOneAsync(filter);
        }
    }
}