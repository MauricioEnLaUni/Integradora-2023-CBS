using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("p")]
    public class ProjectController : FApiControllerBase<Project, ProjectDto, UpdatedProjectDto>
    {
        private new readonly string col = "projects";
        private readonly ProjectionDefinitionBuilder<Project> defPro;
        private readonly FilterDefinitionBuilder<Project> defFil;
        public ProjectController(MongoSettings mongoClient) : base(mongoClient)
        {
            defPro = Builders<Project>.Projection;
            defFil = _repo.filterBuilder;
        }
    }
}