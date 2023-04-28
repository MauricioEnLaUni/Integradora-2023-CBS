using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities.MongoDB;
using System.Security.Claims;
using Fictichos.Constructora.Auth;

namespace Fictichos.Constructora.Repository;

[ApiController]
[Route("[controller]")]
public class AreaController : ControllerBase
{
    private readonly AreaService _areaService;
    private readonly PersonService _peopleService;
    private readonly TokenService _tokenService;
    private readonly UserService _userService;
    private readonly FilterDefinition<Area> empty = Filter.Empty<Area>();

    public AreaController(AreaService area, PersonService people, TokenService token, UserService user)
    {
        _areaService = area;
        _peopleService = people;
        _tokenService = token;
        _userService = user;
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

        bool admin = (usr.Credentials.SingleOrDefault(x => x.Type == "is_admin")?.Value) == "yes";
        if (!admin)
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

        return Ok(_areaService.GetBy(Filter.Empty<Area>()));
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
        
        bool admin = (usr.Credentials.SingleOrDefault(x => x.Type == "is_admin")?.Value) == "yes";

        if (!admin)
        {
            List<string> member = usr
                .Credentials
                .Where(x => x.Type == "member")
                .Select(x => x.Value)
                .ToList();
            if (!member.Contains(id))
            {
                return Forbid();
            }
        }
        
        Area? item = _areaService.GetOneBy(Filter.ById<Area>(id));
        if (item is null) return NotFound();

        return Ok(item);
    }

    [HttpGet("2cCnnv")]
    public IActionResult GetMemberships()
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
        
        List<string> member = usr
            .Credentials
            .Where(x => x.Type == "area_member")
            .Select(x => x.Value)
            .ToList();
        FilterDefinition<Area> filter = Builders<Area>
            .Filter
            .In(x => x.Id, member);

        List<AreaDto> memberships = _areaService.GetByMembers(member);
        
        return Ok(memberships);
    }

    [HttpPost]
    public async Task<IActionResult> CreateArea(NewAreaDto data)
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
        bool areaValidation = await _areaService.ValidateNew(data);
        bool ownerValidation = await _peopleService.ValidateAreaHead(data.Head);

        if (!areaValidation || !ownerValidation) return BadRequest();

        Area result = await _areaService.InsertOneAsync(data);
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = result.Id },
            value: result.ToDto()
        );
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdatedAreaDto data)
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

        await _areaService.ValidateUpdate(data);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOne(string id)
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
        await _areaService.CleanDependencies(id);
        await _areaService.DeleteOneAsync(id);
        return NoContent();
    }
}