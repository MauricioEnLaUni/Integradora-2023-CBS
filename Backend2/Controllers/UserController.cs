using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Abstraction;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IJwtProvider _jwtProvider;
        protected UserService Repo { get; init; }

        public UserController(UserService repo, IJwtProvider jwtProvider)
        {
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
            bool nameIsTaken = await Repo.GetOneByFilterAsync(filter) is null ?
                false : true;
            if (nameIsTaken) return Conflict();

            bool emailIsTaken = await Repo.GetOneByFilterAsync(filter) is null ?
                false : true;
            if (emailIsTaken) return Conflict();

            User raw = await Repo.CreateAsync(payload);
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

            if (!raw.Active) return Forbid();

            LoginSuccessDto data = raw.ToDto();
            string token = _jwtProvider.Generate(data);
            return Ok(token);
        }
    }
}