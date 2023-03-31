using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Abstraction;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Middleware;
using System.Security.Claims;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IJwtProvider _jwtProvider;
    private readonly UserService _userService;
    private readonly EmailService _emailService;

    public UserController(
        MongoSettings container,
        UserService repo,
        EmailService email,
        IJwtProvider jwtProvider)
    {
        _userService = repo;
        _emailService = email;
        _jwtProvider = jwtProvider;
    }

    [HttpPost("new")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] NewUserDto payload)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Name, payload.Name);
        bool nameIsTaken = await _userService
            .GetByFilterAsync(filter) is not null;
        if (nameIsTaken) return Conflict();
        
        string newEmail = payload.Email;
        if (!newEmail.IsEmailFormatted()) return BadRequest();

        var emailFilter = Builders<EmailContainer>.Filter.Eq(e => e.value, newEmail);
        bool emailIsTaken = await _emailService
            .GetEmailByValue(newEmail) is not null;
        if (emailIsTaken) return Conflict();

        User raw = await _userService.InsertOneAsync(payload);

        await _emailService.InsertOneAsync(raw.Id, newEmail);
        
        LoginSuccessDto data = raw.ToDto();

        return new ObjectResult(data)
            { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LoginAsync(LoginDto payload)
    {
        FilterDefinition<User> filter =
            Builders<User>.Filter.Eq(e => e.Name, payload.Name);
        
        User? raw = await _userService.GetByFilterAsync(filter);
        if (raw is null) return NotFound();

        bool passwordMatches = raw.ValidatePassword(payload.Password);
        if (!passwordMatches) return BadRequest();

        if (!raw.Active) return Forbid();
        
        LoginResponseDto response = new(raw, string.Empty);

        response.token = _jwtProvider.Generate(response);

        return Ok(response);
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
        User? usr = await _userService.GetByFilterAsync(filter);
        if (usr is null) return NotFound();
        if (!usr.Active) return Forbid();

        if (data.email is not null) _emailService.ValidateEmailUpdate(data.email, usr);

        usr.UserSelfUpdate(data);
        _userService.Update(filter, usr);
        
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
        User? usr = await _userService.GetByFilterAsync(filter);
        if (usr is null) return NotFound();

        if (data.basicFields.email is not null)
        {
            _emailService.ValidateEmailUpdate(data.basicFields.email, usr);
        }

        usr.Update(data);
        return NoContent();
    }

    [HttpGet("emails")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmails(string id)
    {
        User? test = await _userService.GetByIdAsync(id);
        
        List<EmailContainer> result = await _emailService.GetEmailsByUser(id);

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
        User? info = _userService.GetByFilter(filter);
        return Ok(info);
    }

    [HttpDelete("collection")]
    public IActionResult DeleteCollection()
    {
        _userService.Clear();
        return NoContent();
    }
}