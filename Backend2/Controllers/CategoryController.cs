using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;
using MongoDB.Bson;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using static Fictichos.Constructora.Model.MaterialCategory;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("m")]
    public class MaterialController : FApiControllerBase<MaterialCategory>
    {
        private new readonly string col = "material";
        private ProjectionDefinitionBuilder<MaterialCategory> defMat;
        private FilterDefinitionBuilder<MaterialCategory> defFil;
        public MaterialController(MongoSettings mongoClient) : base(mongoClient)
        {
            defMat = Builders<MaterialCategory>.Projection;
            defFil = _repo.filterBuilder;
        }

        [HttpGet("c/{id}")]
        public async Task<ActionResult<string>> GetByCategoryAsync(string id)
        {
            var projection = defMat.Include(doc => doc.Children);
            var filter = defFil.Where(c => c.Id.Equals(new ObjectId(id)));
            
            List<Material> rawData = await _repo.Collection
                .Find(filter)
                .Project(projection)
                .As<Material>()
                .ToListAsync();
            List<string> result = new();
            rawData.ForEach(e => {
                result.Add(e.AsOverview());
            });
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public override async Task<ActionResult<List<string>>> GetAllAsync()
        {
            List<MaterialCategory> rawData = await _repo.GetAllAsync();
            List<string> data = new();
            rawData.ForEach(e => {
                data.Add(e.AsDto());
            });
            return Ok();
        }
    }
}