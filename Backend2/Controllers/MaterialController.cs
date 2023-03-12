using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("m")]
    public class MaterialController : ControllerBase
    {
        private readonly string db = "cbs";
        private readonly string col = "material";
        private readonly Repository<Material> _repo;
        public MaterialController(MongoSettings mongoClient)
        {
            _repo = new(mongoClient, db, col);
        }
        
        [HttpGet("p")]
        public ActionResult<string> GetAllFromProject(string id)
        {
            return Ok((from m in _repo._col.AsQueryable<Material>()
                where m.Owner == id
                select m.AsOverview()).ToList());
        }

        [HttpGet("c")]
        public ActionResult<string> GetAllFromCategory(string id)
        {
            return Ok((from m in _repo._col.AsQueryable<Material>()
                where m.Category == id
                select m.AsOverview()).ToList());
        }

        [HttpGet("maintenance")]
        public ActionResult<List<string>> GetDamaged()
        {
            return Ok(
                (from m in _repo._col.AsQueryable<Material>()
                where m.Status != 0
                select m.AsMaintenance()).ToList()
            );
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetById(string id)
        {
            Material? item = _repo.GetById(id);
            if(item is null) return NotFound();
            return item.AsOverview();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(string data)
        {
            NewMaterialDto? inputData = JsonConvert.DeserializeObject<NewMaterialDto>(data);
            if (inputData is null) return BadRequest();

            Material newItem = new(inputData);
            await _repo.CreateAsync(newItem);
            return CreatedAtAction(
                nameof(GetById),
                new { id = newItem.Id.ToString() },
                newItem.AsOverview()
            );
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(string data)
        {
            UpdatedMaterialDto? inputData =
                JsonConvert.DeserializeObject<UpdatedMaterialDto>(data);
            if (inputData is null) return BadRequest();

            Material? result = _repo.GetById(inputData.Id);
            if (result is null) return NotFound();

            result.Update(inputData);

            await _repo.UpdateAsync(result);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("bulk")]
        public ActionResult DeleteBulk(List<string> ids)
        {
            ids.ForEach(async (e) => await _repo.DeleteAsync(e));
            return NoContent();
        }
    }
}