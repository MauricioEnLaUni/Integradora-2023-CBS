using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Auth;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class ForeignController : ControllerBase
{
    private readonly CompanyService _companyService;
    private readonly ExternalPersonService _foreignService;
    private readonly TokenService _tokenService;
    private readonly UserService _userService;
    private readonly EmailService _emailService;
    private readonly FilterDefinition<ExternalPerson> empty = Filter.Empty<ExternalPerson>();

    public ForeignController(CompanyService company, ExternalPersonService foreign, TokenService token, UserService users, EmailService email)
    {
        _companyService = company;
        _foreignService = foreign;
        _tokenService = token;
        _userService = users;
        _emailService = email;
    }

    [HttpGet]
    public IActionResult GetAll()
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

        List<string> roles = usr
            .Credentials
            .Where(x => x.Type == "role")
            .Select(x => x.Value)
            .ToList();
        if (!roles.Contains("sales") && !roles.Contains("overseer") && !roles.Contains("manager"))
        {
            return Forbid();
        }

        return Ok(_foreignService.GetOneBy(empty));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
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

        List<string> roles = usr
            .Credentials
            .Where(x => x.Type == "role")
            .Select(x => x.Value)
            .ToList();
        if (!roles.Contains("sales") && !roles.Contains("overseer") && !roles.Contains("manager"))
        {
            return Forbid();
        }

        return Ok(_foreignService.GetOneBy(Filter.ById<ExternalPerson>(id)));
    }

    [HttpPost]
    public IActionResult Create(NewExPersonDto data)
    {
        if (!_emailService.EmailIsAvailable(data.Email)) return BadRequest("Email is invalid");
        ExternalPerson result = new ExternalPerson().Instantiate(data);
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = result.Id},
            value: result.ToDto()
        );
    }

    [HttpPut]
    public IActionResult Update(UpdatedExPersonDto data)
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

        List<string> roles = usr
            .Credentials
            .Where(x => x.Type == "role")
            .Select(x => x.Value)
            .ToList();
        if (!roles.Contains("overseer") && !roles.Contains("manager"))
        {
            return Forbid();
        }
        ExternalPerson? person = _foreignService
            .GetOneBy(Filter.ById<ExternalPerson>(data.Id));
        if (person is null) return NotFound();
        if (data.Contacts is not null && data.Contacts.Emails is not null)
        {
            _emailService.ValidateEmailUpdate(data.Contacts.Emails);
        }
        
        person.Update(data);
        _foreignService.ReplaceOne(Filter.ById<ExternalPerson>(data.Id), person);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
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
        if (usr.Credentials.SingleOrDefault(x => x.Type == "admin")?.Value != "yes")
            return Forbid();

        ExternalPerson? person = _foreignService
            .GetOneBy(Filter.ById<ExternalPerson>(id));
        if (person is null) return NotFound();

        await _companyService.RemovePerson(id, person);

        return NoContent();
    }
}