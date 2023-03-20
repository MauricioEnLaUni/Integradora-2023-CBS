using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;

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
    /// <typeparam name="T">Base controller type.</typeparam>
    /// <typeparam name="U">Container for new object.</typeparam>
    /// <typeparam name="V">Update values.</typeparam>
    [ApiController]
    [Route("[controller]")]
    public abstract class FApiControllerBase<T, U, V, W> : ControllerBase
    where T : BaseEntity, IQueryMask<T, U, V>, new()
    where V : DtoBase
    where W : BaseRepositoryService<T, U, V>
    {
        protected readonly string db = "cbs";
        protected readonly string col = "users";
        protected abstract W Repo { get; init; }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync(
            [FromBody] U payload)
        {
            T data = await Repo.CreateAsync(payload);

            return CreatedAtAction(
                actionName: nameof(GetByIdAsync),
                routeValues: JsonConvert.SerializeObject(new { id = data.Id }),
                value: data.Serialize()
            );
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> GetAllAsync()
        {
            List<T> rawData = await Repo.GetAllAsync();
            List<string> data = new();
            rawData.ForEach(e => {
                data.Add(e.Serialize());
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
            return Ok(result.Serialize());
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(
            [FromBody] V payload)
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