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

        if (!usr.IsAdmin())
        {
            List<string> roles = usr
                .Credentials
                .Where(x => x.Type == "role")
                .Select(x => x.Value)
                .ToList();
            if (!roles.Contains("sales") && !roles.Contains("overseer") && !roles.Contains("manager"))
                return Forbid();
        }

        return Ok(_foreignService.GetOneBy(empty));
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
        
        List<ExternalPerson>? members = _companyService
            .GetOneBy(Filter.ById<Company>(id))?
            .Members;
        List<PersonCondensedDto> result = new();
        members?.ForEach(e => result.Add(e.ToCondensed()));

        return Ok(result);
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

        List<ExternalPerson>  raw = _foreignService.GetBy(Filter.ById<ExternalPerson>(id));
        List<PersonCondensedDto> result = new();
        raw.ForEach(e => {
            result.Add(e.ToCondensed());
        });

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(NewExPersonDto data)
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
            List<string> roles = usr.GetRoles();
            if (!roles.Contains("sales") && !roles.Contains("manager"))
                return Forbid();
        }

        if (!_emailService.EmailIsAvailable(data.Email))
            return BadRequest("Email is invalid");
        ExternalPerson result = _foreignService.InsertOne(data);
        _companyService.AddMember(data);

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

        if (!usr.IsAdmin())
        {
            List<string> roles = usr.GetRoles();
            if (!roles.Contains("sales") && !roles.Contains("overseer") && !roles.Contains("manager"))
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

    [HttpPatch("address")]
    public IActionResult UpdateAddress(NewAddressDto data)
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
            List<string> roles = usr.GetRoles();
            if (!roles.Contains("manager"))
                return Forbid();
        }

        ExternalPerson? raw = _foreignService.GetOneBy(Filter.ById<ExternalPerson>(data.Id));

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

        if (!usr.IsAdmin())
            return Forbid();

        ExternalPerson? person = _foreignService
            .GetOneBy(Filter.ById<ExternalPerson>(id));
        if (person is null) return NotFound();

        await _companyService.RemovePerson(id, person);

        return NoContent();
    }
}