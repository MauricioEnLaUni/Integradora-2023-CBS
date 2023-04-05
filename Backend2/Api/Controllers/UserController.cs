using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Abstraction;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Middleware;
using System.Security.Claims;
using Fictichos.Constructora.Utilities.MongoDB;
using Fictichos.Constructora.Auth;
using System.IdentityModel.Tokens.Jwt;

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
        await _emailService.InsertOneAsync(wrapper);
        
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
        CookieOptions cookieOptions = new()
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddMinutes(1),
            SameSite = SameSiteMode.Strict
        };

        Response.Cookies.Append("Fictichos_Session", token, cookieOptions);
        Response.Cookies.Append("Fictichos_Claims", token, new()
        {
            HttpOnly = false,
            Expires = DateTime.Now.AddMinutes(1),
            SameSite = SameSiteMode.Strict
        });
        
        _tokenService.AddToken(token);

        return Ok(token);
    }

    [HttpGet("refresh")]
    public IActionResult Refresh()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ","");
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);
        return Ok();
    }

    [HttpPost("logout")]
    public IActionResult LogOut()
    {
        Response.Cookies.Append("Fictichos_Session", "", new() {
            HttpOnly = true,
            Expires = DateTime.Now.AddMinutes(-10)
        });
        Response.Cookies.Append("Fictichos_Claims", "", new()
        {
            HttpOnly = false,
            Expires = DateTime.Now.AddMinutes(-10)
        });
        return Ok();
    }
    
    [HttpPatch("self")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SelfUpdate(
        [FromBody] UserSelfUpdateDto data)
    {
        IEnumerable<Claim> claims = TokenValidator.GetClaims(data.token);
        string? name = claims.Where(x => x.Type == "unique_name")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (name is null) return BadRequest();

        var filter = Builders<User>.Filter.Eq(x => x.Name, name);
        User? usr = await _userService.GetOneByAsync(filter);
        if (usr is null) return NotFound();
        if (!usr.Active) return Forbid();

        if (data.email is not null) _emailService.ValidateEmailUpdate(data.email);

        usr.UserSelfUpdate(data);
        _userService.ReplaceOne(filter, usr);
        
        return NoContent();
    }

    [HttpPatch("admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateUser([FromBody] UserAdminUpdateDto data)
    {
        var filter = Builders<User>.Filter
            .Eq(x => x.Name, data.name);
        User? usr = await _userService.GetOneByAsync(filter);
        if (usr is null) return NotFound();

        if (data.basicFields.email is not null)
        {
            _emailService.ValidateEmailUpdate(data.basicFields.email);
        }

        usr.Update(data);
        return NoContent();
    }

    [HttpGet("emails")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmails(string id)
    {
        User? test = await _userService.GetOneByAsync(Filter.ById<User>(id));
        if (test is null) return NotFound();
        
        FilterDefinition<EmailContainer> filter = Builders<EmailContainer>
            .Filter
            .Eq(x => x.owner, id);
        List<EmailContainer> result = await _emailService.GetByAsync(filter);

        List<string> output = new();
        
        result?.ForEach(e => {
            output.Add(e.value);
        });

        return Ok(output);
    }

    [HttpGet("userInfo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetUser(string name)
    {
        var filter = Builders<User>.Filter.Eq(x => x.Name, name);
        User? info = _userService.GetOneBy(filter);
        return Ok(info);
    }

    [HttpDelete("collection")]
    public IActionResult DeleteCollection()
    {
        _userService.Clear();
        return NoContent();
    }
}