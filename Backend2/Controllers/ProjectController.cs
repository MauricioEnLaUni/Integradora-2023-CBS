using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fitichos.Constructora.Repository;
using Fictichos.Constructora.Model;
using Fitichos.Constructora.Dto;

namespace Fitichos.Constructora.Controllers
{
    [ApiController]
    [Route("p")]
    public class ProjectController : ControllerBase
    {
        private readonly Repository<Project, NewProjectDto> _repo;
        private readonly string db = "cbs";
        private readonly string col = "projects";
        public ProjectController(IMongoClient mongoClient)
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

        [HttpPost("task/new")]
        public async Task<ActionResult> AddTask(NewFTaskDto data)
        {
            Project? exists = _repo.GetById(data.Id);
            if (exists is null) return NotFound();

            exists.Tasks.Add(new(data));
            await _repo.UpdateAsync(exists);
            return Ok();
        }

        [HttpDelete("task/delete")]
        public async Task<ActionResult> DeleteTask(NewFTaskDto data)
        {
            Project? exists = _repo.GetById(data.Id);
            if (exists is null) return NotFound();

            exists.Tasks.Remove(new(data));
            await _repo.UpdateAsync(exists);
            return Ok();
        }

        [HttpPut("task/update")]
        public async Task<ActionResult> UpdateTask(UpdateFTaskDto data)
        {
            Project? exists = _repo.GetById(data.Owner);
            if (exists is null) return NotFound();

            ObjectId _id = new(data.Id);
            int index = exists.Tasks.FindIndex(t => t.Id == _id);
            if (index is -1) return NotFound();

            exists.Tasks[index].Update(data);
            await _repo.UpdateAsync(exists);
            return Ok();
        }
    }
}