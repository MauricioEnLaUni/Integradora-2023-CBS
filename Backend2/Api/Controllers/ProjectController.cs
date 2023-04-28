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
    private readonly AccountService _accountService;
    private readonly TokenService _tokenService;
    private readonly UserService _userService;

    public ProjectController(
        ProjectService container,
        PersonService person,
        MaterialService material,
        TokenService token,
        UserService user,
        AccountService account)
    {
        _projectService = container;
        _materialService = material;
        _personService = person;
        _tokenService = token;
        _userService = user;
        _accountService = account;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
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

        List<Project> raw = await _projectService
            .GetByAsync(Filter.Empty<Project>());
        List<ProjectDto> result = _projectService.ToDtoList(raw);
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
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

    [HttpPost("company/accounts")]
    public IActionResult PostAccounts([FromBody] List<string> ids)
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

        List<string> raw = new();
        List<Account> rawAccount = new();
        List<AccountDto> result = new();

        ids.ForEach(e => {
            string? t = _projectService.GetOneBy(e)?.PayHistory;
            if (t is not null)
                raw.Add(t);
        });

        raw.ForEach(e => {
            rawAccount.Add(_accountService.GetOneBy(e)!);
        });

        rawAccount.ForEach(e => {
            result.Add(e.ToDto());
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> NewProject([FromBody] string data)
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
    public IActionResult Update(
        [FromBody] UpdatedProjectDto data)
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
        
        return NoContent();
    }

    [HttpDelete("collection")]
    public IActionResult DeleteCollection()
    {
        _projectService.Clear();
        return NoContent();
    }
}