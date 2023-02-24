using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fictichos.Constructora.Database
{
    public class Connector
  {
        private MongoClientSettings Settings { get; init; }
        public MongoClient Client { get; init; }

        public Connector()
        {
            var ConnectionString = Environment.GetEnvironmentVariable("MONGODB__SECRET");
            if (ConnectionString is null) throw new Exception("Invalid Connection String.");

            Settings = MongoClientSettings.FromConnectionString(ConnectionString);
            Settings.LinqProvider = LinqProvider.V3;
            Client = new MongoClient(Settings);
        }
    }
}