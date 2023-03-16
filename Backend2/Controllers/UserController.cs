using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly string db = "cbs";
        private readonly string col = "users";
        private readonly RepositoryAsync<User, LoginSuccessDto> _repo;
        public UserController(MongoSettings mongoClient)
        {
            _repo = new(mongoClient, db, col);
        }

        [HttpPost("new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> RegisterAsync(
          [FromBody] string payload)
        {
            User data = await _repo.CreateAsync(payload);
            return CreatedAtAction(
              nameof(Login),
              new { usr = data.Name },
              new { username = data.Name }
            );
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Login(
            [FromBody] string payload)
        {
            LoginDto? id = JsonConvert.DeserializeObject<LoginDto>(payload);
            if (id is null) return BadRequest();

            var filter = _repo.filterBuilder.Eq(e => e.Name, id.Name);
            User? data = await _repo.GetOneByFilterAsync(filter);
            if (data is null) return NotFound();

            if (!data.ValidatePassword(id.Password)) return StatusCode(400);
            if (!data.Active) return Unauthorized();

            return Ok(data.SerializeDto());
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(string payload)
        {
            if (payload is null) return BadRequest();

            UserChangesDto? id = JsonConvert
                .DeserializeObject<UserChangesDto>(payload);
            if (id is null) return BadRequest();

            User? rawData = await _repo.GetByIdAsync(id.Id);
            if (rawData is null) return NotFound();

            rawData.Update(id);
            
            await _repo.UpdateAsync(rawData);
            return NoContent();
        }
    }
}