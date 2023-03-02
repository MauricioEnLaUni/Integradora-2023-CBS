using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fitichos.Constructora.Controllers
{
    [ApiController]
    [Route("t")]
    public class FTasksController : ControllerBase
    {
        private readonly Repository<FTasks, NewFTaskDto> _repo;
        private readonly string db = "cbs";
        private readonly string col = "tasks";
        private readonly IMongoCollection<FTasks> prCol;
        public FTasksController(MongoSettings mongoClient)
        {
            _repo = new(mongoClient, db, col);
            prCol = _repo._col;
        }

        [HttpGet("all")]
        public ActionResult<List<FTasks>> AllTasks()
        {
            try
            {
                return
                    Ok((from t in prCol.AsQueryable()
                    select t).ToList());
            } catch(Exception)
            {
                return StatusCode(503);
            }
        }

        [HttpGet("todo/{id}")]
        public ActionResult<List<FTasks>> ToDo(string? id)
        {
            if (id is null)
            return Ok(
                (from t in prCol.AsQueryable()  
                  where t.Closed > DateTime.Now
                  select t).ToList());

            ObjectId _id = new(id);
            return
                (from t in prCol.AsQueryable()
                where t.Closed > DateTime.Now &&
                t.Parent == _id
                select t).ToList();
        }

        [HttpGet("complete/{id}")]
        public ActionResult<List<FTasks>> Done(string? id)
        {
            if (id is null)
            return Ok(
                (from t in prCol.AsQueryable()
                where t.Closed < DateTime.Now
                select t).ToList());

            ObjectId _id = new(id);
            return Ok(
                (from t in prCol.AsQueryable()
                where t.Parent == _id && 
                t.Closed < DateTime.Now
                select t).ToList());
        }

        [HttpGet("fullTask/{id}")]
        public ActionResult<List<FTasks>> FullTask(string id)
        {
            ObjectId _id = new(id);
            return Ok(
                (from t in prCol.AsQueryable()
                where t.Parent == _id
                select t).ToList());
        }
    }
}