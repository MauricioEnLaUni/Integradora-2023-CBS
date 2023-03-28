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
    }
}