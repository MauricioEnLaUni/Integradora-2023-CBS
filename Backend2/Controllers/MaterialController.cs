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
        private readonly Repository<Material, NewMaterialDto> _repo;
        private readonly IMongoCollection<Material> _col;
        public MaterialController(IMongoClient mongoClient)
        {
            _col = mongoClient.GetDatabase("cbs").GetCollection<Material>("material");
            _repo = new(mongoClient);
        }
    }
}