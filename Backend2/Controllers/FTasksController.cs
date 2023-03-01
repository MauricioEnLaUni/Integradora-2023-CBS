using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;
using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Controllers
{
    [ApiController]
    [Route("t")]
    public class FTasksController : ControllerBase
    {
        private string databaseName = "cbs";
        private readonly string collectionName = "projects";
        private readonly IMongoCollection<Project> prCol;
        public FTasksController(IMongoClient mongoClient)
        {
            prCol = mongoClient.GetDatabase("db")
                .GetCollection<Project>("projects");
        }

        [HttpGet("all")]
        public List<List<FTasks>> AllTasks()
        {
            return
                (from p in prCol.AsQueryable()
                select p.Tasks).ToList();
        }

        [HttpGet("todo/{id}")]
        public List<FTasks> ToDo(string? id)
        {
            if (id is null)
            return
                (from p in prCol.AsQueryable()
                    from t in p.Tasks
                    where t.Closed > DateTime.Now
                    select t).ToList();

            ObjectId _id = new(id);
            return
                (from p in prCol.AsQueryable()
                where p.Id == _id
                    from t in p.Tasks
                    where t.Closed > DateTime.Now
                    select t).ToList();
        }

        [HttpGet("complete/{id}")]
        public List<FTasks> Done(string? id)
        {
            if (id is null)
            return
                (from p in prCol.AsQueryable()
                    from t in p.Tasks
                    where t.Closed < DateTime.Now
                    select t).ToList();

            ObjectId _id = new(id);
            return
                (from p in prCol.AsQueryable()
                where p.Id == _id
                    from t in p.Tasks
                    where t.Closed < DateTime.Now
                    select t).ToList();
        }

        [HttpGet("fullTask/{id}")]
        public List<FTasks> FullTask(string id)
        {
            ObjectId _id = new(id);
            return
                (from p in prCol.AsQueryable()
                    from t in p.Tasks
                    where t.Id == _id
                    select t).ToList();
        }
    }
}