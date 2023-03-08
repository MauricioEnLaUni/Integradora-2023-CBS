using Microsoft.AspNetCore.Mvc;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fitichos.Constructora.Controllers
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

        [HttpGet]
        public List<MaterialDto> GetAll()
        {
            List<Material> source = _repo.GetAll();
            return (from m in source
            select new MaterialDto(m)).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<MaterialDto?> GetById(string id)
        {
            Material? result = _repo.GetById(id);
            if(result is null) return NotFound();
            MaterialDto? data = new(result);
            return Ok(data);
        }

        [HttpGet("b/{id}")]
        public ActionResult<List<MaterialDto>> GetByBrand()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<MaterialDto>> AddMaterial(NewMaterialDto data)
        {
            Material toInsert = new(data);
            await _repo.CreateAsync(toInsert);
            return Ok(new MaterialDto(toInsert));
        }

        [HttpPost("i")]
        public async Task<ActionResult<MaterialDto>> ImportMaterial(Material data)
        {
            await _repo.CreateAsync(data);
            return Ok(new MaterialDto(data));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMaterial(UpdatedMaterialDto data)
        {
            if (data.Quantity is null && data.Status is null
                && data.BoughtFor is null) return BadRequest();

            Material? update = _repo.GetById(data.Id);
            if (update is null) return NotFound();

            await _repo.UpdateAsync(update);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOne(string id)
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