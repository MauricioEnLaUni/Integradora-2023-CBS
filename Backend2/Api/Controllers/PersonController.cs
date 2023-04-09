using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonService _personService;
    private readonly ProjectService _projectService;
    private readonly TimeTrackerService _time;
    private readonly EmailService _emailService;
    private readonly AreaService _areaService;

    public PersonController(
        PersonService person,
        ProjectService project,
        EmailService email,
        TimeTrackerService time,
        AreaService area)
    {
        _personService = person;
        _projectService = project;
        _time = time;
        _emailService = email;
        _areaService = area;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        List<Person> raw = _personService.GetBy(Filter.Empty<Person>());
        List<PersonDto> result = new();
        raw.ForEach(e => {
            result.Add(e.ToDto());
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(string id)
    {
        Person? output = _personService
            .GetOneBy(Filter.ById<Person>(id));
        if (output is null) return NotFound();

        return Ok(output.ToDto());
    }

    [HttpGet("project/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByProject(string id)
    {
        Project? project = _projectService
            .GetOneBy(Filter.ById<Project>(id));
        if (project is null) return NotFound();

        FilterDefinition<Person> filter = Builders<Person>
            .Filter
            .AnyEq("Charges.Assignments", project.Id);
        List<Person> raw = _personService
            .GetBy(filter);
        List<PersonDto> result = new();
        raw.ForEach(e => {
            result.Add(e.ToDto());
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateEmployee(NewPersonDto data)
    {
        if (!_time.Over(0, data.DOB)) return BadRequest("Employee must be at least over 16 years old.");
        if (data.Email is not null && !_emailService.EmailIsAvailable(data.Email))
            return Conflict("Email already in use.");
        
        NewPersonDto? validated = _personService.ValidateNewPerson(data);


        Person? newItem = _personService.InsertOne(data);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = newItem },
            value: newItem.ToDto()
        );
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdatePerson(UpdatedPersonDto data)
    {
        Person? item = _personService
            .GetOneBy(Filter.ById<Person>(data.Id));
        if (item is null) return NotFound();

        item.Update(data);

        return NoContent();
    }
}