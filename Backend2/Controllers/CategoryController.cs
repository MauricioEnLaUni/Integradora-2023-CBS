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
    public class MaterialController<MaterialCategory> : FApiControllerBase<MaterialCategory>
    {
        private new readonly string col = "materialCategory";
        public MaterialController(MongoSettings mongoClient) : base(mongoClient) { }
    }
}