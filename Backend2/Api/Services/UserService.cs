using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using MongoDB.Driver;

namespace Fictichos.Constructora.Repository
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;
        public UserService(MongoSettings container)
        {
            _userCollection = container.Client.GetDatabase("cbs")
                .GetCollection<User>("users");
        }

        public async Task<User?> GetUser(string data)
        {
            if (data is null) return null;

            FilterDefinition<User> filter = Builders<User>.Filter
                .Eq(x => x.Id, data);
            User? result = await _userCollection.Find(filter)
                .SingleOrDefaultAsync();
                
            return result;
        }

        #region Deletion
        public HTTPResult<List<string>> ValidateDelete(string id)
        {
            User? usr = GetById(id);
            if (usr is null) return new() { Code = 404 };
            
            return new() { Code = 200, Value = usr.Email };
        }

        public void Clear()
        {
            _userCollection.DeleteManyAsync(_ => true);
        }

        public User? GetById(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter
                .Eq(x => x.Id, id);
            return _userCollection.Find(filter).SingleOrDefault();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter
                .Eq(x => x.Id, id);
            return await _userCollection
                .Find(filter)
                .SingleOrDefaultAsync();
        }

        public async Task<User?> GetByFilterAsync(FilterDefinition<User> filter)
        {
            return await _userCollection
                .Find(filter)
                .SingleOrDefaultAsync();
        }

        public User? GetByFilter(FilterDefinition<User> filter)
        {
            return _userCollection
                .Find(filter)
                .SingleOrDefault();
        }

        #endregion

        #region Validate New
        public async Task<HTTPResult<string>> NameIsUnique(string name)
        {
            FilterDefinition<User> filter = Builders<User>
                .Filter
                .Eq(x => x.Name, name);
            if (await GetByFilterAsync(filter) is null)
                return new(){ Code = 409 };
            return new() { Code = 204 };
        }

        public async Task<User> InsertOneAsync(NewUserDto data)
        {
            User newItem = new(data);
            await _userCollection.InsertOneAsync(newItem);
            return newItem;
        }

        #endregion

        #region Update
        public void Update(FilterDefinition<User> filter, User data)
        {
            _userCollection.ReplaceOne(filter, data);
        }
        #endregion
    }
}