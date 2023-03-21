using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Abstraction;
using Fictichos.Constructora.Utilities;

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
        EmailCollection =
            container.Client.GetDatabase("cbs").GetCollection<EmailContainer>("emails");
        Repo = repo;
        _jwtProvider = jwtProvider;
    }

    [HttpPost("new")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] NewUserDto payload)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Name, payload.Name);
        var projection = Builders<User>.Projection.Include(e => e.Name);
        bool nameIsTaken = await Repo.GetOneByFilterAsync(filter) is not null;
        if (nameIsTaken) return Conflict();

        string newEmail = payload.Email;
        var emailFilter = Builders<EmailContainer>.Filter.Eq(e => e.Value, newEmail);
        bool emailIsTaken = await EmailCollection.Find(emailFilter)
            .SingleOrDefaultAsync() is not null;
        if (emailIsTaken) return Conflict();

        User raw = await Repo.CreateAsync(payload);

        EmailContainer email = new(newEmail);
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
        if (passwordMatches is false) return BadRequest();

        if (!raw.Active) return Forbid();
        
        LoginResponseDto response = new(raw, string.Empty);

        response.token = _jwtProvider.Generate(response);

        return Ok(response);
    }

    [HttpPost("u")]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(
        [FromBody] LoginDto payload)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Name, payload.Name);
        User? raw = await Repo.GetOneByFilterAsync(filter);
        if (raw is null) return NotFound();
        if (raw.ValidatePassword(payload.Password)) return BadRequest();

        LoginSuccessDto data = raw.ToDto();
        return Ok(data);
    }
}