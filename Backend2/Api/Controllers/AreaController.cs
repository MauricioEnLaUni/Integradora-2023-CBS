using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository;

[ApiController]
[Route("[controller]")]
public class AreaController : ControllerBase
{
    private readonly AreaService _areaService;
    private readonly PersonService _peopleService;

    public AreaController(AreaService area, PersonService people)
    {
        _areaService = area;
        _peopleService = people;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_areaService.GetBy(Filter.Empty<Area>()));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        Area? item = _areaService.GetOneBy(Filter.ById<Area>(id));
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateArea(NewAreaDto data)
    {
        bool areaValidation = await _areaService.ValidateNew(data);
        bool ownerValidation = await _peopleService.ValidateAreaHead(data.Head);

        if (!areaValidation || !ownerValidation) return BadRequest();

        Area result = await _areaService.InsertOneAsync(data);
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = result.Id },
            value: result.ToDto()
        );
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdatedAreaDto data)
    {
        await _areaService.ValidateUpdate(data);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOne(string id)
    {
        await _areaService.CleanDependencies(id);
        return NoContent();
    }
}