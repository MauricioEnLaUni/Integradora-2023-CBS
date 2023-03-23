using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Abstraction;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Middleware;
using System.Security.Claims;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class MaterialController : ControllerBase
{
    private IMongoCollection<MaterialCategory> CategoryCollection { get; init; }
    private IMongoCollection<Material> MaterialCollection { get; init; }

    public MaterialController(MongoSettings container)
    {
        MaterialCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Material>("material");
        CategoryCollection = container.Client.GetDatabase("cbs")
            .GetCollection<MaterialCategory>("materialCategory");
    }

    [HttpPost("cats")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateMaterialCatAsync(
        [FromBody] NewMaterialCategoryDto data)
    {
        var filter = Builders<MaterialCategory>.Filter
            .Eq(x => x.Name, data.Name);
        MaterialCategory? cat = await CategoryCollection.Find(filter)
            .SingleOrDefaultAsync();
        if (cat is not null) return Conflict();

        MaterialCategory result = await CategoryCollection
            .CreateAsync<MaterialCategory, NewMaterialCategoryDto, UpdatedMatCategoryDto>(data);
        
        return new ObjectResult(result.Serialize())
            { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPost("subCat")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateSubcategory(
        [FromBody] NewMaterialCategoryDto data)
    {
        if (data.Parent is null) return BadRequest();
        var filter = Builders<MaterialCategory>.Filter
            .Eq(x => x.Id, data.Parent);
        MaterialCategory? parent = await CategoryCollection.Find(filter)
            .SingleOrDefaultAsync();

        filter = Builders<MaterialCategory>.Filter
            .Eq(x => x.Parent, data.Parent);
            
        List<MaterialCategory> peers = await CategoryCollection.Find(filter)
            .ToListAsync();
        if (peers.Any(x => x.Name == data.Name)) return Conflict();

        CategoryCollection.

        
        return new ObjectResult()
            { StatusCode = StatusCodes.Status201Created };
    }
}