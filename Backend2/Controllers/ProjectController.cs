using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fitichos.Constructora.Repository;
using Fictichos.Constructora.Model;
using Fitichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;

namespace Fitichos.Constructora.Controllers
{
    [ApiController]
    [Route("p")]
    public class ProjectController : ControllerBase
    {
        private readonly Repository<Project, NewProjectDto> _repo;
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
    }
}