using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("t")]
    public class FTasksController : ControllerBase
    {
        private readonly Repository<FTasks> _repo;
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

        [HttpPost]
        public async Task<ActionResult<FTasks>> CreateTask(NewFTaskDto data)
        {
            FTasks newTask = new(data);
            await _repo.CreateAsync(newTask);
            return Ok(newTask);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask(UpdatedFTaskDto data)
        {
            if (data.Name is null && data.StartDate is null && data.Address is null
                && data.Subtasks is null && data.EmployeesAssigned is null
                    && data.Material is null && data.Closed is null) return BadRequest();

            FTasks? item = _repo.GetById(data.Id);
            if (item is null) return NotFound();

            item.Update(data);
            await _repo.UpdateAsync(item);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTask(string id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}