using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService projectService;
    private readonly IMongoCollection<Project> projectCollection;

    public ProjectController(ProjectService container)
    {
        projectService = container;
        projectCollection = container.projectCollection;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await projectCollection.Find(x => true)
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        FilterDefinition<Project> filter = Builders<Project>
            .Filter.Eq(x => x.Id, id);
        Project? raw = await projectCollection.GetOneByFilterAsync(filter);
        if (raw is null) return NotFound();

        return Ok(raw.ToDto());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> NewProject([FromBody] string data)
    {
        if (await projectService.NameIsUnique(data)) return Conflict();

        Project? result = await projectCollection
            .CreateAsync<Project, string, UpdatedProjectDto>(data);
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = result.Id },
            value: projectService.To(result)
        );
    }

    [HttpDelete("collection")]
    public async Task<IActionResult> DeleteCollection()
    {
        await projectCollection.DeleteManyAsync(_ => true);
        return NoContent();
    }
    
}