using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Auth;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities.MongoDB;
using System.Security.Claims;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly PersonService _personService;
    private readonly MaterialService _materialService;
    private readonly TokenService _tokenService;
    private readonly UserService _userService;

    public ProjectController(
        ProjectService container,
        PersonService person,
        MaterialService material,
        TokenService token,
        UserService user)
    {
        _projectService = container;
        _materialService = material;
        _personService = person;
        _tokenService = token;
        _userService = user;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        string? token = Request.Cookies["fid"];
        if (token is null) return Unauthorized();
        string? sub = _tokenService.CookieAuth(token);
        if (sub is null) return Forbid();
        
        User? auth = _userService
            .AuthRoles(sub, null, new() { "user" } );
        if (auth is null) return Forbid();

        List<Project> raw = await _projectService
            .GetByAsync(Filter.Empty<Project>());
        List<ProjectDto> result = _projectService.ToDtoList(raw);
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        string? token = Request.Cookies["fid"];
        if (token is null) return Unauthorized();
        string? sub = _tokenService.CookieAuth(token);
        if (sub is null) return Forbid();
        
        User? auth = _userService
            .AuthRoles(sub, null, new() { "user" } );
        if (auth is null) return Forbid();

        FilterDefinition<Project> filter = Builders<Project>
            .Filter.Eq(x => x.Id, id);
        Project? raw = await _projectService.GetOneByAsync(filter);
        if (raw is null) return NotFound();

        return Ok(raw.ToDto());
    }

    [HttpGet("company/{id}")]
    public IActionResult GetByCompany(string id)
    {
        string header = HttpContext.Request.Headers["Authorization"]!;
        if (header is null) return Unauthorized();
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return BadRequest();

        User? usr = _userService.GetOneBy(Filter.ById<User>(idCredential));
        if (usr is null) return NotFound();
        if (!usr.Active) return Forbid();

        if (!usr.IsAdmin())
        {
            List<string> roles = usr
                .Credentials
                .Where(x => x.Type == "role")
                .Select(x => x.Value)
                .ToList();
            if (!roles.Contains("sales") && !roles.Contains("overseer") && !roles.Contains("manager"))
            {
                return Forbid();
            }
        }
        
        FilterDefinition<Project> filter = Builders<Project>
            .Filter
            .Eq(x => x.Owner, id);
        List<Project> raw = _projectService.GetBy(filter);
        List<ProjectDto> result = new();
        raw.ForEach(e => result.Add(e.ToDto()));

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> NewProject([FromBody] string data)
    {
        string? token = Request.Cookies["fid"];
        if (token is null) return Unauthorized();
        string? sub = _tokenService.CookieAuth(token);
        if (sub is null) return Forbid();
        
        User? auth = _userService
            .AuthRoles(sub, null, new() { "manager", "admin"} );
        if (auth is null) return Forbid();

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
            .GetOneByAsync(projectFilter);
        if (original is null) return NotFound();

        HTTPResult<UpdatedProjectDto?> projectValidation =
            await _projectService.ValidateUpdate(data, original);
            
        if (projectValidation.Code == 400) return BadRequest();

        UpdatedProjectDto validated = projectValidation.Value!;
        FilterDefinition<Person> respFilter = Builders<Person>
            .Filter
            .Eq(x => x.Id, validated.Responsible);
        Person? responsible = await _personService
            .GetOneByAsync(respFilter);
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