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
            string User = Environment.GetEnvironmentVariable("DOCKER__MONGODB__USER")!;
            string Password = Environment.GetEnvironmentVariable("DOCKER__MONGODB__PASS")!;
            string Port = Environment.GetEnvironmentVariable("MONGODB__DATABASE__PORT")!;
            string Host = Environment.GetEnvironmentVariable("MONGODB__DATABASE__HOST")!;
            var settings = MongoClientSettings.FromConnectionString($"mongodb://{User}:{Password}@{Host}:{Port}");
            settings.LinqProvider = LinqProvider.V3;
            Client = new(settings);
        }

        public IMongoCollection<IMongoMask> MakeCollection(int key)
        {
            return Client.GetDatabase("cbs")
                .GetCollection<IMongoMask>(Collections[key]);
        }
    }

    public static class Constants
    {
        public const string COOKIENAME = "LachterCookie";
    }
}