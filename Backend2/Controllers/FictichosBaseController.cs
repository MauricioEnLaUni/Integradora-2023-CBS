using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class FApiControllerBase<T, U, V, W, X> : ControllerBase
    where T : AbstractEntity<T, U, V>, new()
    where W : IUpdateDto
    where X : BaseRepositoryService<T, U, V>
    {
        protected readonly string db = "cbs";
        protected readonly string col = "users";
        protected abstract X Repo { get; init; }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync(
            [FromBody] string payload)
        {
            T data = await Repo.CreateAsync(payload);

            return CreatedAtAction(
                actionName: nameof(GetByIdAsync),
                routeValues: new { id = data.Id.ToString() },
                value: data.ToDto()
            );
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> GetAllAsync()
        {
            List<T> rawData = await Repo.GetAllAsync();
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
            T? result = await Repo.GetByIdAsync(new ObjectId(id));
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
            W update = JsonConvert.DeserializeObject<W>(payload)!;

            T? data = await Repo.GetByIdAsync(update.Id);
            if (data is null) return NotFound();

            data.Update(update);
            await Repo.UpdateAsync(data);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await Repo.DeleteAsync(new ObjectId(id));
            return NoContent();
        }

        [HttpDelete("bulk")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        public ActionResult DeleteBulk(
            [FromBody] List<string> payload)
        {
            payload.ForEach(async e => {
                await Repo.DeleteAsync(new ObjectId(e));
            });
            return NoContent();
        }
    }
}