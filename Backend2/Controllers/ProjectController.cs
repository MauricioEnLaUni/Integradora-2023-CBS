using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("p")]
    public class ProjectController : ControllerBase
    {
        private readonly Repository<Project> _repo;
        private readonly string db = "cbs";
        private readonly string col = "projects";
        public ProjectController(MongoSettings mongoClient)
        {
            _repo = new(mongoClient, db, col);
        }

        [HttpGet]
        public List<Project> GetAll()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public Project? GetById(string id)
        {
            return _repo.GetById(id);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Project>> CreateNewProject(NewProjectDto newProject)
        {
            Project toInsert = new(newProject);
            await _repo.CreateAsync(toInsert);
            return Ok(toInsert);
        }

        [HttpPut]
        public async Task<ActionResult> Update(ProjectChangesDto changes)
        {
            ObjectId _id = new(changes.Id);
            Project? exists =
                (from u in _repo._col.AsQueryable()
                where u.Id == _id
                select u).SingleOrDefault();
            if (exists is null) return NotFound();

            exists.Change(changes);

            await _repo.UpdateAsync(exists);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("bulk")]
        public ActionResult DeleteBulk(List<string> ids)
        {
            ids.ForEach(async (e) => await _repo.DeleteAsync(e));
            return NoContent();
        }
    }
}