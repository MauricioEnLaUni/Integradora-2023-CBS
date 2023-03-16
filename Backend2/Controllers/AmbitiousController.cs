using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FApiControllerBase<T, U, V> : ControllerBase
    where T : Entity, IQueryMask<T, U, V>, new()
    where V : DtoBase
    {
        protected readonly string db = "cbs";
        protected readonly string col = "users";
        protected readonly RepositoryAsync<T, U, V> _repo;

        public FApiControllerBase(MongoSettings mongoClient)
        {
            _repo = new(mongoClient, db, col);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync(
            [FromBody] string payload)
        {
            T data = await _repo.CreateAsync(payload);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = data.Id.ToString() },
                data.ToDto()
            );
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> GetAllAsync()
        {
            List<T> rawData = await _repo.GetAllAsync();
            List<string> data = new();
            rawData.ForEach(e => {
                data.Add(e.SerializeDto());
            });
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string?>> GetByIdAsync(string id)
        {
            T? result = await _repo.GetByIdAsync(new ObjectId(id));
            if (result is null) return NotFound($"Document: ${id} does not exist in ${col} collection.");
            return Ok(result.SerializeDto());
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(string payload)
        {
            if (payload is null) return BadRequest();
            V update = JsonConvert.DeserializeObject<V>(payload)!;

            T? data = await _repo.GetByIdAsync(update.Id);
            if (data is null) return NotFound();

            data.Update(update);
            await _repo.UpdateAsync(data);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _repo.DeleteAsync(new ObjectId(id));
            return NoContent();
        }

        [HttpDelete("bulk")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        public ActionResult DeleteBulk(
            [FromBody] List<string> payload)
        {
            payload.ForEach(async e => {
                await _repo.DeleteAsync(new ObjectId(e));
            });
            return NoContent();
        }
    }
}