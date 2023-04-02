using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly PersonService _personService;
    private readonly MaterialService _materialService;

    public ProjectController(
        ProjectService container,
        PersonService person,
        MaterialService material)
    {
        _projectService = container;
        _materialService = material;
        _personService = person;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _projectService
            .GetByFilterAsync(Filter.Empty<Project>())
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        FilterDefinition<Project> filter = Builders<Project>
            .Filter.Eq(x => x.Id, id);
        Project? raw = await _projectService.GetOneByFilterAsync(filter);
        if (raw is null) return NotFound();

        return Ok(raw.ToDto());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> NewProject([FromBody] string data)
    {
        if (!await _projectService.NameIsUnique(data)) return Conflict();

        Project result = await _projectService.InsertOneAsync(data);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = result.Id },
            value: _projectService.To(result)
        );
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        [FromBody] UpdatedProjectDto data)
    {
        FilterDefinition<Project> projectFilter = Builders<Project>
            .Filter
            .Eq(x => x.Id, data.Id);
        Project? original = await _projectService
            .GetOneByFilterAsync(projectFilter);
        if (original is null) return NotFound();

        HTTPResult<UpdatedProjectDto?> projectValidation =
            await _projectService.ValidateUpdate(data, original);
            
        if (projectValidation.Code == 400) return BadRequest();

        UpdatedProjectDto validated = projectValidation.Value!;
        FilterDefinition<Person> respFilter = Builders<Person>
            .Filter
            .Eq(x => x.Id, validated.Responsible);
        Person? responsible = await _personService
            .GetOneByFilterAsync(respFilter);
        validated.Responsible = responsible?.Id;
        
        return NoContent();
    }

    [HttpDelete("collection")]
    public IActionResult DeleteCollection()
    {
        _projectService.Clear();
        return NoContent();
    }
}