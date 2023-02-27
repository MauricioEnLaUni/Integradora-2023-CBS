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
    [Route("project")]
    public class ProjectController : ControllerBase, IActionable<ProjectInfoDTO, NewProjectDTO, UpdatedProjectDTO>
    {
        private Connector<Project> _conn = new(0, "projects");

        private List<Project> SelectAll()
        {
            return
                (from p in _conn.Collection.AsQueryable()
                select p).ToList<Project>();
        }

        [HttpGet]
        public ActionResult<List<ProjectInfoDTO>> GetAll()
        {
            List<Project> list = SelectAll();
            List<ProjectInfoDTO> results = list.Select(p => p.AsInfoDTO()).ToList();
            return Ok(results);
        }

        private Project? SelectById(string id)
        {
            ObjectId _id = new ObjectId(id);
            List<Project> projects = SelectAll();

            return
                (from p in projects
                where p.Id == _id
                select p).SingleOrDefault<Project>();
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetById(string id)
        {
            Project? project = SelectById(id);
            if (project is null) return NotFound();

            return Ok(project.AsInfoDTO());
        }

        [HttpPost]
        public ActionResult<ProjectInfoDTO> Insert(NewProjectDTO newProject)
        {
            Project project = new(newProject);

            try
            {
                _conn.Collection.InsertOne(project);
                return CreatedAtAction(nameof(GetById), new { id = project.Id.ToString() }, project.AsInfoDTO());
            } catch(Exception)
            {
                Console.WriteLine("An error has occurred while processing your request!");
                return StatusCode(500);
            }
        }

        [HttpPut]
        public ActionResult<ProjectInfoDTO> Update(UpdatedProjectDTO newData)
        {
            if (newData.Name is null && newData.Closed is null
                && newData.PayHistory is null && newData.Tasks is null)
            {
                return StatusCode(400);
            }

            Project? projectToUpdate = SelectById(newData.id);
            if (projectToUpdate is null) return NotFound();

            var filter = Builders<Project>.Filter.Eq("Id", projectToUpdate.Id);
            var update = Builders<Project>.Update
                .Set(p => p.Name, newData.Name is not null ? newData.Name : projectToUpdate.Name)
                .Set(p => p.Closed, newData.Closed is not null ? newData.Closed : projectToUpdate.Closed)
                .Set(p => p.PayHistory, newData.PayHistory is not null ? newData.PayHistory : projectToUpdate.PayHistory)
                .Set(p => p.Tasks, newData.Tasks is not null ? newData.Tasks : projectToUpdate.Tasks);

            _conn.Collection.UpdateOne(filter, update);
            
            return NoContent();
        }

        [HttpDelete]
        public ActionResult<ProjectInfoDTO> Delete(string id)
        {
            Project? projectToDelete = SelectById(id);
            if (projectToDelete is null) return NotFound();

            var filter = Builders<Project>.Filter.Eq("Id", projectToDelete.Id);
            _conn.Collection.DeleteOne(filter);

            return NoContent();
        }
    }
}