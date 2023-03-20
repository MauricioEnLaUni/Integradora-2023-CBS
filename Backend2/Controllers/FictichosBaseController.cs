using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Controllers
{
    /// <summary>
    /// Main engine for the Api, does all CRUD operations for generics.
    /// This is generally applied for general use, although some classes have
    /// their own Controller, such as in Users where we want to keep finer
    /// control.
    /// Provides support for searches with the GetByFilterAsync.
    /// </summary>
    /// <remarks>
    /// Every in the controller operation is Asynchronous.
    /// </remarks>
    /// <typeparam name="T">The type the controller poperates with. It is
    /// necessary for the collection to search for these types.</typeparam>
    /// <typeparam name="U">Base Dto type, can probably be refactored into an
    /// interface.</typeparam>
    /// <typeparam name="V">Data for creating new Ts through instantiate.
    /// </typeparam>
    /// <typeparam name="W">Data for the Update method.</typeparam>
    /// <typeparam name="X">Service for repository.</typeparam>
    [ApiController]
    [Route("[controller]")]
    public abstract class FApiControllerBase<T, U, V, W, X> : ControllerBase
    where T : AbstractEntity<T, U, V>, new()
    where W : DtoBase
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
                routeValues: JsonConvert.SerializeObject(new { id = data.Id }),
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
            T? result = await Repo.GetByIdAsync(id);
            if (result is null) return NotFound($"Document: ${id} does not exist in ${col} collection.");
            return Ok(result.SerializeDto());
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(
            [FromBody] W payload)
        {
            if (payload is null) return BadRequest();
            

            T? data = await Repo.GetByIdAsync(payload.Id);
            if (data is null) return NotFound();

            data.Update(payload);
            await Repo.UpdateAsync(data);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await Repo.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("bulk")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes
            .Status204NoContent)]
        public ActionResult DeleteBulk(
            [FromBody] List<string> payload)
        {
            payload.ForEach(async e => {
                await Repo.DeleteAsync(e);
            });
            return NoContent();
        }
    }
}