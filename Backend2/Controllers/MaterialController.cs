using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;
using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Controllers
{
    [ApiController]
    [Route("m")]
    public class MaterialController : ControllerBase
    {
        private readonly string db = "cbs";
        private readonly string col = "material";
        private readonly Repository<Material, NewMaterialDto> _repo;
        public MaterialController(IMongoClient mongoClient)
        {
            _repo = new(mongoClient, db, col);
        }

        [HttpGet]
        public List<Material> GetAll()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Material?> GetById(string id)
        {
            Material? result = _repo.GetById(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Material>> AddMaterial(NewMaterialDto data)
        {
            Material toInsert = new(data);
            await _repo.CreateAsync(toInsert);
            return Ok(toInsert);
        }
    }
}