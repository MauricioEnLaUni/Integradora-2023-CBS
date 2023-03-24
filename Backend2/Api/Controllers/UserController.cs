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
    private IMongoCollection<EmailContainer> EmailCollection { get; init; }
    private readonly IJwtProvider _jwtProvider;
    protected UserService Repo { get; init; }

    public UserController(UserService repo, IJwtProvider jwtProvider, MongoSettings container)
    {
        EmailCollection = repo.EmailCollection;
        Repo = repo;
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
        var projection = Builders<User>.Projection.Include(e => e.Name);
        bool nameIsTaken = await Repo.GetOneByFilterAsync(filter) is not null;
        if (nameIsTaken) return Conflict();
        
        string newEmail = payload.Email;
        if (!newEmail.IsEmailFormatted()) return BadRequest();

        var emailFilter = Builders<EmailContainer>.Filter.Eq(e => e.value, newEmail);
        bool emailIsTaken = await EmailCollection.Find(emailFilter)
            .SingleOrDefaultAsync() is not null;
        if (emailIsTaken) return Conflict();

        User raw = await Repo.CreateAsync(payload);

        EmailContainer email = new(raw.Id, newEmail);
        await EmailCollection.InsertOneAsync(email);
        
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
        
        User? raw = await Repo.GetOneByFilterAsync(filter);
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
        User? usr = await Repo.GetOneByFilterAsync(filter);
        if (usr is null) return NotFound();
        if (!usr.Active) return Forbid();

        if (data.email is not null) Repo.ValidateEmail(data.email, usr);

        usr.UserSelfUpdate(data);
        await Repo.UpdateAsync(usr);
        
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
        User? usr = await Repo.GetOneByFilterAsync(filter);
        if (usr is null) return NotFound();

        if (data.basicFields.email is not null)
        {
            Repo.ValidateEmail(data.basicFields.email, usr);
        }

        usr.Update(data);
        return NoContent();
    }

    [HttpGet("emails")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmails(string id)
    {
        User? test = await Repo.GetByIdAsync(id);

        var filter = Builders<EmailContainer>
            .Filter.Eq(e => e.owner, id);
        List<EmailContainer> result = await EmailCollection.Find(filter)
            .ToListAsync();

        List<string> output = new();
        
        result?.ForEach(e => {
            output.Add(e.value);
        });

        return Ok(output);
    }

    [HttpGet("userInfo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser(string name)
    {
        var filter = Builders<User>.Filter.Eq(x => x.Name, name);
        User? info = await Repo.GetOneByFilterAsync(name);
        return Ok(info);
    }

    [HttpDelete("collection")]
    public async Task<IActionResult> DeleteCollection()
    {
        await Repo.Collection.DeleteManyAsync(_ => true);
        return NoContent();
    }
}