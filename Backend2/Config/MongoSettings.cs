using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fictichos.Constructora.Utilities
{
    public class MongoSettings
    {
        public MongoClient Client;
        public MongoSettings()
        {
            //string User = Environment.GetEnvironmentVariable("DOCKER__MONGODB__USER")!;
            //string Password = Environment.GetEnvironmentVariable("DOCKER__MONGODB__PASS")!;
            //string Port = Environment.GetEnvironmentVariable("MONGODB__DATABASE__PORT")!;
            //string Host = Environment.GetEnvironmentVariable("MONGODB__DATABASE__HOST")!;
            var settings = MongoClientSettings.FromConnectionString(
              Environment.GetEnvironmentVariable("MONGODB__SECRET")
            );
            // var settings = MongoClientSettings.FromConnectionString($"mongodb://{User}:{Password}@{Host}:{Port}");
            settings.LinqProvider = LinqProvider.V3;
            Client = new(settings);
        }
    }
}