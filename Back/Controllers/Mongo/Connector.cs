using Fictichos.Constructora.Utils.Generics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fictichos.Constructora.Database
{
    public class Connector : ControllerBase
  {
        private MongoClientSettings Settings { get; init; }
        private MongoClient Client { get; init; }

        public Connector()
        {
            var ConnectionString = Environment.GetEnvironmentVariable("MONGODB__SECRET");
            if (ConnectionString is null) throw new Exception("Invalid Connection String.");

            Settings = MongoClientSettings.FromConnectionString(ConnectionString);
            Settings.LinqProvider = LinqProvider.V3;
            Client = new MongoClient(Settings);
        }

        public ActionResult<List<ILinqSearchable>> Query(
            string db,
            string col,
            string field,
            IPrimitives value
        ) {
            IMongoCollection<ILinqSearchable> searchIn = Client.GetDatabase(db).GetCollection<ILinqSearchable>(col);
            IMongoQueryable<ILinqSearchable> results =
                from member in searchIn.AsQueryable()
                where member.GetType().GetProperty(field)!.GetValue(member) == value
                select member;
            if (results is null)
            {
                return NotFound();
            }

            return Ok(results);
        }
        public IMongoQueryable<ILinqSearchable> Query(
            string db,
            string col,
            string field,
            string value
        ) {
            IMongoCollection<ILinqSearchable> searchIn = Client.GetDatabase(db).GetCollection<ILinqSearchable>(col);
            IMongoQueryable<ILinqSearchable> results =
                from member in searchIn.AsQueryable()
                where member.GetType().GetProperty(field).GetValue(member).ToString() == value
                select member;

            return results;
        }
    }
}