using Fictichos.Constructora.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Fictichos.Constructora.Repository;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonService _personService;
    private readonly ProjectService _projectService;

    public PersonController(
        PersonService person,
        ProjectService project)
    {
        _personService = person;
        _projectService = project;
    }

    [HttpGet]
    public List<Person> GetAll()
    {
        FilterDefinition<Person> filter = Builders<Person>
        .Filter
        .Where(_ => true);
        return _personService.GetBy(filter);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(string id)
    {
        FilterDefinition<Person> filter = Builders<Person>
            .Filter
            .Eq(x => x.Id, id);
        Person? output = _personService.GetOneBy(filter);
        if (output is null) return NotFound();

        return Ok(output);
    }

    [HttpGet("employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetEmployees()
    {
        FilterDefinition<Person> filter = Builders<Person>
            .Filter
            .Where(x => x.Employed != null);
        return Ok(_personService.GetBy(filter));
    }

    [HttpGet("{project}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByProject(string id)
    {
        FilterDefinition<Project> projectFilter = Builders<Project>
            .Filter
            .Eq(x => x.Id, id);
        Project? project = _projectService.GetOneBy(projectFilter);
        if (project is null) return NotFound();

        return Ok();
    }

    [HttpGet("{relation}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByRelations(string relation)
    {
        FilterDefinition<Person> filter = Builders<Person>
            .Filter
            .Eq(x => x.Relation, relation);
        return Ok(_personService.GetBy(filter));
    }
}