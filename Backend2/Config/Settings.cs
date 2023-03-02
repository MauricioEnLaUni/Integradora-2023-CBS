using MongoDB.Driver;
using MongoDB.Driver.Linq;
using dotenv.net;

namespace Fictichos.Constructora.Utilities
{
    public record DotEnvManager
    {
        public IDictionary<string, string> Env { get; init; }
        public DotEnvManager()
        {
            DotEnv.Load();
            Env = DotEnv.Read();
        }
    }

    public class MongoSettings
    {
        public MongoClient Client;
        private readonly List<string> Collections = new()
        {
            "testCollection",
            "users",
            "people",
            "project",
            "material",
            "tasks",
            "accounts"
        };
        public MongoSettings()
        {
            var settings = MongoClientSettings.FromConnectionString(
                Environment.GetEnvironmentVariable("MONGODB__SECRET")
            );
            settings.LinqProvider = LinqProvider.V3;
            Client = new(settings);
        }

        public IMongoCollection<IMongoMask> MakeCollection(int key)
        {
            return Client.GetDatabase("cbs")
                .GetCollection<IMongoMask>(Collections[key]);
        }
    }
}