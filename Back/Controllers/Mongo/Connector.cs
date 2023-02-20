using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fictichos.Constructora.Database
{
    public class Connector
    {
        private MongoClientSettings settings { get; init; }
        private MongoClient client { get; init; }

        public Connector(string ConnectionString)
        {
            this.settings = MongoClientSettings.FromConnectionString(
                Environment.GetEnvironmentVariable(ConnectionString)
            );
            this.settings.LinqProvider = LinqProvider.V3;
            this.client = new MongoClient(settings);
        }
    }
}