using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fictichos.Constructora.Database
{
    public class Connector<T>
  {
        private MongoClientSettings Settings { get; init; }
        public MongoClient Client { get; init; }
        public IMongoCollection<T> Collection {get; set; }
        private List<string> Databases { get; init; } = new List<string>()
        {
            "MONGODB__DATABASE"
        };
        private Dictionary<string,string> Collections { get; init; } =
            new Dictionary<string, string>()
        {
            { "people", "MONGODB__DATABASE__COLLECTION__PEOPLE"},
            { "projects", "MONGODB__DATABASE__COLLECTION__PROJECTS"},
            { "accounts", "MONGODB__DATABASE__COLLECTION__ACCOUNTS"},
            { "material", "MONGODB__DATABASE__COLLECTION__MATERIALS" }
        };

        public Connector(int db, string cn)
        {
            string[] Credentials = CreateCredentials(db, cn);
            Settings = MongoClientSettings.FromConnectionString(Credentials[0]);
            Settings.LinqProvider = LinqProvider.V3;
            Client = new MongoClient(Settings);
            Collection =
                    this.Client.GetDatabase(Credentials[1])
                            .GetCollection<T>(Credentials[2]);
        }

        private string[] CreateCredentials(int db, string cn)
        {
            var ConnectionString =
                Environment.GetEnvironmentVariable("MONGODB__SECRET");
            var Database =
                Environment.GetEnvironmentVariable(Databases[db]);
            var Collection = 
                Environment.GetEnvironmentVariable(Collections[cn]);
            if (ConnectionString is null ||
                    Database is null ||
                            Collection is null)
            {
                throw new Exception("Missing Database Credentials!");
            }
            return new string[]
            {
                ConnectionString,
                Database,
                Collection
            };
        }

        public void SetCollection(int db, string cn)
        {
            string[] Credentials = CreateCredentials(db, cn);
            Collection =
                    this.Client.GetDatabase(Credentials[1])
                            .GetCollection<T>(Credentials[2]);

        }
    }
}