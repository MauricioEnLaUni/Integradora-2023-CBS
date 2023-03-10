using Microsoft.AspNetCore.Mvc;

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
        private readonly Repository<MaterialCategory> _repo;
        public MaterialController(MongoSettings mongoClient)
        {
            _repo = new(mongoClient, db, col);
        }

        [HttpGet]
        public List<MaterialDto> GetAll()
        {
            List<MaterialCategory> source = _repo.GetAll();
            List<MaterialCategory> list =
                (from m in source
                select m).ToList();
            List<MaterialDto> value = new();
            list.ForEach(l => {
                l.Children.ForEach(m => {
                    value.Add(m.AsOverview());
                });
            });
            return value;
        }

        [HttpGet("{id}")]
        public ActionResult<MaterialDto?> GetById(string id)
        {
            Material? result = _repo.GetById(id);
            if(result is null) return NotFound();
            MaterialDto? data = result.AsOverview();
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
            return Ok(toInsert.AsOverview());
        }

        [HttpPost("i")]
        public async Task<ActionResult<MaterialDto>> ImportMaterial(Material data)
        {
            await _repo.CreateAsync(data);
            return Ok(data.AsOverview());
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