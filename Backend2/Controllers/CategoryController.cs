using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;
using MongoDB.Bson;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("m")]
    public class MaterialCategoryController : ControllerBase
    {
        private readonly Repository<MaterialCategory> _repo;
        private readonly string db = "cbs";
        private readonly string col = "materialCategory";
        public MaterialCategoryController(MongoSettings mongoClient)
        {
            _repo = new(mongoClient, db, col);
        }

        [HttpGet]
        public ActionResult<List<string>> GetAll()
        {
            List<MaterialCategory> rawData = _repo.GetAll();
            return Ok(
                (from m in rawData
                select m.AsDto()).ToList()
            );
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetById(string id)
        {
            MaterialCategory? rawData = _repo.GetById(id);
            if (rawData is null) return NotFound();

            return Ok(rawData.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult>
            NewCategoryAsync(NewMaterialCategoryDto data)
        {
            MaterialCategory newItem = new(data);
            await _repo.CreateAsync(newItem);
            return CreatedAtAction(
                nameof(GetById),
                new { id = newItem.Id.ToString() },
                newItem.AsDto()
            );
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOneAsync(UpdateMatCategoryDto data)
        {
            MaterialCategory? rawData = _repo.GetById(data.Id);
            if (rawData is null) return NotFound();

            rawData.Update(data);

            await _repo.UpdateAsync(rawData);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOneAsync(string id)
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