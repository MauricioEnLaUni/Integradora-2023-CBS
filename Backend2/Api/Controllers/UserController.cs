using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Abstraction;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Auth;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IJwtProvider _jwtProvider;
    private readonly UserService _userService;
    private readonly EmailService _emailService;
    private readonly TokenService _tokenService;

    public UserController(
        UserService repo,
        EmailService email,
        TokenService tokens,
        IJwtProvider jwtProvider)
    {
        _userService = repo;
        _emailService = email;
        _tokenService = tokens;
        _jwtProvider = jwtProvider;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] NewUserDto payload)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Name, payload.Name);
        if (!_userService.NameIsUnique(payload.Name)) return Conflict();
        
        string newEmail = payload.Email;
        if (!newEmail.IsEmailFormatted()) return BadRequest();

        if (!_emailService.EmailIsAvailable(newEmail))
            return Conflict();

        User raw = await _userService.InsertOneAsync(payload);
        
        NewEmailDto wrapper = new() { owner = raw.Id, value = newEmail };
        EmailContainer mail = await _emailService.InsertOneAsync(wrapper);
        _userService.GrantEmail(mail.Id, raw.Id);
        
        LoginSuccessDto data = raw.ToDto();

        return new ObjectResult(data)
            { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPost("auth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LoginAsync(LoginDto payload)
    {
        FilterDefinition<User> filter =
            Builders<User>.Filter.Eq(e => e.Name, payload.Name);
        
        User? raw = await _userService.GetOneByAsync(filter);
        if (raw is null) return NotFound();

        bool passwordMatches = raw.ValidatePassword(payload.Password);
        if (!passwordMatches) return BadRequest();

        if (!raw.Active) return Forbid();

        string token = _jwtProvider.Generate(raw);
        _tokenService.AddToken(token);
        
        HttpContext.Response.Headers.Add("Authorization", "Bearer " + token);

        return Ok();
    }

    [HttpGet("refresh")]
    public IActionResult Refresh()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ","");
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);
        
        return Ok(jwtToken);
    }

    [HttpPost("logout")]
    public IActionResult LogOut()
    {
        string accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");
        FilterDefinition<TokenContainer> filter = Builders<TokenContainer>
            .Filter.Eq(x => x.Token, accessToken);
        _tokenService.DeleteOne(filter);
        
        return NoContent();
    }
    
    [HttpPatch("self")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult SelfUpdate(
        [FromBody] UserSelfUpdateDto data)
    {
        string header = HttpContext.Request.Headers["Authorization"]!;
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return BadRequest();

        User? usr = _userService.GetOneBy(Filter.ById<User>(idCredential));
        if (usr is null) return NotFound();
        if (!usr.Active) return Forbid();

        // BUG: Doesn't validate if you can remove an email
        Dictionary<string, string> policy =
            new() {{ "unique_name", data.Name }};
        if (data.email is not null)
        {
            data.email.ForEach(e => {
                if (e.Operation != 1 && e.NewItem is null)
                {
                    data.email.Remove(e);
                } else if (e.Operation != 1)
                {
                    policy.Add("owner", e.NewItem! );
                }
            });
        }

        bool? validation = _tokenService
            .AuthorizeAll(usr.Credentials, header, policy);
        if (validation is not true) return Forbid();

        if (data.email is not null) _emailService.ValidateEmailUpdate(data.email);

        usr.UserSelfUpdate(data);
        _userService.ReplaceOne(Filter.ById<User>(usr.Id), usr);
        
        return NoContent();
    }

    [HttpPatch("admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult UpdateUser([FromBody] UserAdminUpdateDto data)
    {
        string header = HttpContext.Request.Headers["Authorization"]!;
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return BadRequest();

        User? usr = _userService.GetOneBy(Filter.ById<User>(idCredential));
        if (usr is null) return NotFound();
        if (!usr.Active) return Forbid();
        if (!usr.IsAdmin()) return Forbid();

        if (data.basicFields.email is not null)
        {
            _emailService.ValidateEmailUpdate(data.basicFields.email);
        }

        usr.Update(data);
        _userService.ReplaceOne(Filter.ById<User>(usr.Id), usr);
        return NoContent();
    }
    
    [HttpGet("emails")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmails(string id)
    {
        string header = HttpContext.Request.Headers["Authorization"]!;
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return BadRequest();

        User? usr = _userService.GetOneBy(Filter.ById<User>(idCredential));
        if (usr is null) return NotFound();

        Dictionary<string, string> policy = new() {{ "owner", id }};
        bool? validation = _tokenService
            .AuthorizeAll(usr.Credentials, header, policy);
        if (validation is not true) return Forbid();

        FilterDefinition<EmailContainer> filter = Builders<EmailContainer>
            .Filter
            .Eq(x => x.owner, id);
        List<EmailContainer> result = await _emailService.GetByAsync(filter);

        List<string> output = new();
        
        result?.ForEach(e => {
            output.Add(e.value);
        });

        return Ok();
    }

    [HttpGet("userInfo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetUser(string name)
    {
        string header = HttpContext.Request.Headers["Authorization"]!;
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return Unauthorized();

        User? usr = _userService.GetOneBy(Filter.ById<User>(idCredential));
        if (usr is null) return NotFound();
        if (usr.Name != claims
            .SingleOrDefault(x => x.Type == "unique_name")?.Value)
                return Forbid();
        
        return Ok(usr.ToDto());
    }

    [HttpDelete("collection")]
    public IActionResult DeleteCollection()
    {
        string header = HttpContext.Request.Headers["Authorization"]!;
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return BadRequest();

        User? usr = _userService.GetOneBy(Filter.ById<User>(idCredential));
        if (usr is null) return NotFound();
        if (!usr.Active) return Forbid();
        if (!usr.IsAdmin()) return Forbid();
        
        _userService.Clear();
        return NoContent();
    }
}