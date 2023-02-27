using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.DTOs;
using Fictichos.Constructora.Models;
using Fictichos.Constructora.Database;

namespace Fictichos.Constructora.Controller
{
    [ApiController]
    [Route("task")]
    public class FTasksController : ControllerBase, IActionable<FTaskInfoDTO, NewFTaskDTO, FTaskInfoDTO>
    {
        private Connector<Project> _conn = new(0, "project");
        
        private IMongoQueryable<Project> GetAll()
        {
            return 
                from project in _conn.Collection.AsQueryable()
                select project;
        }

        private Project? ProjectExists(ObjectId id)
        {
            IMongoQueryable<Project> list = GetAll();
            return
                (from p in list
                where p.Id == id
                select p).SingleOrDefault<Project>();
        }

        [HttpPost("new")]
        public ActionResult<FTaskInfoDTO> Insert(NewFTaskDTO newTask)
        {
            FTasks taskToAdd = new(newTask);
            var filter = Builders<Project>.Filter.Eq("id", newTask.Owner);
            var update = Builders<Project>.Update
                .Push<FTasks>(u => u.Tasks, taskToAdd);
            _conn.Collection.UpdateOne(filter, update);
            
            return Ok();
        }

        [HttpPut]
        public ActionResult<FTaskInfoDTO> Update(FTaskInfoDTO newData)
        {
            return new FTaskInfoDTO();
        }

        [HttpDelete]
        public ActionResult<FTaskInfoDTO> Delete(string usr)
        {
            return new FTaskInfoDTO();
        }
    }
}