using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using System.Net.Mime;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class MaterialController : ControllerBase
{
    private readonly MongoSettings Container;
    private IMongoCollection<MaterialCategory> CategoryCollection { get; init; }
    private IMongoCollection<Material> MaterialCollection { get; init; }

    public MaterialController(MongoSettings container)
    {
        Container = container;
        MaterialCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Material>("material");
        CategoryCollection = container.Client.GetDatabase("cbs")
            .GetCollection<MaterialCategory>("materialCategory");
    }

    [HttpPost("cats")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateMaterialCatAsync(
        [FromBody] NewMaterialCategoryDto data)
    {
        if (data is null) return BadRequest();
        MaterialCategory result = new();
        if (data.Parent is null)
        {
            var filter = Builders<MaterialCategory>.Filter
            .Eq(x => x.Name, data.Name);
        MaterialCategory? cat = await CategoryCollection.Find(filter)
            .SingleOrDefaultAsync();
        if (cat is not null) return Conflict();

        result = await CategoryCollection
            .CreateAsync<MaterialCategory, NewMaterialCategoryDto,
                UpdatedMatCategoryDto>(data);
        }
        else
        {
            var filter = Builders<MaterialCategory>.Filter
            .Eq(x => x.Id, data.Parent);
            MaterialCategory? parent = await CategoryCollection.Find(filter)
                .SingleOrDefaultAsync();

            filter = Builders<MaterialCategory>.Filter
                .Eq(x => x.Parent, data.Parent);
                
            List<MaterialCategory> peers = await CategoryCollection.Find(filter)
                .ToListAsync();
            if (peers.Any(x => x.Name == data.Name)) return Conflict();

            result = new MaterialCategory().Instantiate(data);

            UpdateDefinition<MaterialCategory> update =
                Builders<MaterialCategory>.Update
                    .Push(x => x.SubCategory,
                        result.Id);

            using (var session = Container.Client.StartSession())
            {
                var cancellationToken = CancellationToken.None;
                var results = session.WithTransaction(
                    async (s, ct) => 
                    {
                        await CategoryCollection
                            .InsertOneAsync(result, cancellationToken: ct);
                        await CategoryCollection
                            .FindOneAndUpdateAsync(filter, update);
                        return true;
                    }
                );
            }
        }
        
        return new ObjectResult(result.Serialize())
            { StatusCode = StatusCodes.Status201Created };
    }

    [HttpGet("roots")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoots()
    {
        var filter = Builders<MaterialCategory>.Filter
            .Where(x => x.Parent == null);
        List<MaterialCategory> rawData = await CategoryCollection.Find(filter)
            .ToListAsync();
        List<MaterialCategoryDto> result = new();
        rawData.ForEach(e => result
            .Add(e.To<MaterialCategory, MaterialCategoryDto>()));

        return Ok(result);
    }

    [HttpGet("children")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChildren(string data)
    {
        FilterDefinition<MaterialCategory> categoryFilter =
            Builders<MaterialCategory>.Filter.Eq(x => x.Parent, data);

        List<MaterialCategory> rawCategories = await CategoryCollection
            .GeyByFilterAsync(categoryFilter);
        
        FilterDefinition<Material> materialFilter = Builders<Material>
            .Filter.Eq(x => x.Parent, data);

        List<Material> rawMaterial = await MaterialCollection
            .GeyByFilterAsync<Material>(materialFilter);
        
        List<MaterialCategoryDto> catResult = new();
        rawCategories.ForEach(e => {
            catResult.Add(e.To<MaterialCategory, MaterialCategoryDto>());
        });

        List<MaterialDto> matResult = new();
        rawMaterial.ForEach(e => {
            matResult.Add(e.To<Material, MaterialDto>());
        });

        return Ok(new MaterialChildren {
            categories = catResult,
            material = matResult
        });
    }

    [HttpGet("project")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByProject(string id)
    {
        FilterDefinition<Material> filter = Builders<Material>.Filter
            .Eq(x => x.Owner, id);
        List<Material> rawData = await MaterialCollection
            .GeyByFilterAsync(filter);

        List<MaterialDto> result = new();
        rawData.ForEach(e => {
            result.Add(e.To<Material, MaterialDto>());
        });
        return Ok();
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateOne(
        [FromBody] UpdatedMatCategoryDto data)
    {
        FilterDefinition<MaterialCategory> filter =
            Builders<MaterialCategory>.Filter.Eq(x => x.Id, data.Id);
        MaterialCategory? raw = await CategoryCollection.Find(filter)
            .SingleOrDefaultAsync();
        if (raw is null) return BadRequest();

        raw.Update(data);
        raw.ModifiedAt = DateTime.Now;
        await CategoryCollection.ReplaceOneAsync(filter, raw);
        return NoContent();
    }

    [HttpDelete("collection")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCollection()
    {
        FilterDefinition<MaterialCategory> filter =
            Builders<MaterialCategory>.Filter.Eq(x => true, true);

        await CategoryCollection.DeleteManyAsync(filter);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string data)
    {
        await CategoryCollection.DeleteAsync(data);
        return NoContent();
    }

}