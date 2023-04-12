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
public class CompaniesController : ControllerBase
{
    private readonly CompanyService _companyService;
    private readonly ExternalPersonService _foreignService;
    private readonly TokenService _tokenService;
    private readonly UserService _userService;
    private readonly EmailService _emailService;
    private readonly FilterDefinition<Company> empty = Filter.Empty<Company>();

    public CompaniesController(CompanyService company, ExternalPersonService foreign, TokenService token, UserService users, EmailService email)
    {
        _companyService = company;
        _foreignService = foreign;
        _tokenService = token;
        _userService = users;
        _emailService = email;
    }

    [HttpPost]
    public IActionResult Create(
        [FromBody] NewCompanyDto data)
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

        Company cmp = _companyService.InsertOne(data);

        if (data.Email is not null)
        {
            _emailService.EmailIsAvailable(data.Email);
            _emailService
                .InsertOne(new() { owner = cmp.Id, value = data.Email });
        }
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = cmp.Id },
            value: cmp.ToDto()
        );
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
            {
                return Forbid();
            }
        }

        List<Company> raw = _companyService.GetBy(empty);
        List<CompanyBrowserDto> result = new();
        raw.ForEach(e => result.Add(e.ToBrowserDto()));

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

        CompanyDto? result = _companyService.GetOneBy(id)?.ToDto();
        if (result is null) return NotFound();

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update([FromBody] UpdatedCompanyDto data)
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

        Company? original = _companyService
            .GetOneBy(Filter.ById<Company>(data.Id));
        if (original is null) return NotFound();

        if (data.Contacts is not null && data.Contacts.Emails is not null)
        {
            _emailService.ValidateEmailUpdate(data.Contacts.Emails);
        }
        original.Update(data);
        _companyService.ReplaceOne(Filter.ById<Company>(data.Id), original);

        return NoContent();
    }

    [HttpDelete("{id}")]
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
        {
            List<string> roles = usr
                .Credentials
                .Where(x => x.Type == "role")
                .Select(x => x.Value)
                .ToList();
            if (!roles.Contains("overseer") && !roles.Contains("manager"))
            {
                return Forbid();
            }
        }

        Company? cmp = _companyService
            .GetOneBy(Filter.ById<Company>(id));
        if (cmp is null) return NotFound();
        FilterDefinition<ExternalPerson> filter = Builders<ExternalPerson>
            .Filter
            .Where(x => cmp.Members.Contains(x));
        await _foreignService.DeleteManyAsync(filter);
        await _companyService.DeleteOneAsync(Filter.ById<Company>(cmp.Id));

        return NoContent();
    }
}