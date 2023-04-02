using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly FTaskService _taskService;
    private readonly PersonService _peopleService;
    private readonly ProjectService _projectService;
    private readonly MaterialService _materialService;
    internal TaskController(
        FTaskService service,
        PersonService people,
        ProjectService project,
        MaterialService material
    )
    {
        _taskService = service;
        _peopleService = people;
        _projectService = project;
        _materialService = material;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] NewFTaskDto data)
    {
        List<string> rawEmployee = (await _peopleService
            .GetByAsync(data.Overseer))
            .Select(x => x.Id)
            .ToList();
        List<string> rawProject = (await _projectService
            .GetByAsync(Filter.Empty<Project>()))
            .Select(x => x.Id)
            .ToList();
        List<string> Assigned = data.Assignees;
        HTTPResult<NewFTaskDto> taskValidation =
            _taskService.ValidateNew(data, rawEmployee);
        Assigned.ForEach(e => {
            if (!rawEmployee.Contains(e)) Assigned.Remove(e);
        });

        if (taskValidation.Code == 409) return Conflict();
        if (taskValidation.Code == 400
            || Assigned.Count < 1
            || !Assigned.Contains(data.Overseer))
                return BadRequest();
        if (data.Parent is not null && !rawProject.Contains(data.Parent))
            return NotFound();

        NewFTaskDto payload = taskValidation.Value!;
        payload.Assignees = Assigned;
        
        FTasks newItem = await _taskService.InsertOneAsync(payload);


        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = newItem.Id },
            value: newItem.ToDto()
        );
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_taskService.GetBy(Filter.Empty<FTasks>()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(string id)
    {
        FTasks? item =  _taskService
            .GetOneBy(Filter.ById<FTasks>(id));
        IActionResult result = item is null ? NotFound() : Ok(item.ToDto());

        return result;
    }

    [HttpGet("{project}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByProject(string id)
    {
        Project? item =  _projectService
            .GetOneBy(Filter.ById<Project>(id));
        if (item is null) return NotFound();

        return Ok(_taskService.ToDtoList(item.Tasks));
    }

    [HttpDelete("collection")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Clear()
    {
        _taskService.Clear();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteById(string id)
    {
        _taskService.DeleteOne(id);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ReplaceOneAsync(UpdatedFTaskDto data)
    {
        FTasks? original = await _taskService
            .GetOneByAsync(Filter.ById<FTasks>(data.Id));
        if (original is null) return NotFound();

        Project? owner = await _projectService
            .GetOneByAsync(Filter.ById<Project>(original.Owner));
        if (owner is null) return NotFound();

        List<string> employees = new();
        List<string> materials = new();
        FilterDefinition<Person> employeeFilter = Builders<Person>
            .Filter
            .Exists(x => x.Employed);

        employees = (await _peopleService
                .GetByAsync(employeeFilter))
                .Select(x => x.Id)
                .ToList();
        
        materials = (await _materialService
            .GetByFilterAsync(Filter.Empty<Material>()))
            .Select(x => x.Id)
            .ToList();
        
        UpdatedFTaskDto result =
            _taskService.ValidateUpdate(data, employees, original, materials);
        original.Update(result);
        _taskService.ReplaceOne(Filter.ById<FTasks>(data.Id), original);
        
        return NoContent();
    }

    [HttpPatch("{material}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMaterial(
        TaskSingleUpdateList<string> data)
    {
        List<string>? original = (await _taskService
            .GetOneByAsync(Filter.ById<FTasks>(data.Id)))?
            .Material;
        List<string> materials = (await _materialService
            .GetByFilterAsync(Filter.Empty<Material>()))
            .Select(x => x.Id)
            .ToList();
        if (original is null) return NotFound();

        data.changes.ForEach(e => {
            if (e.NewItem is null || !materials.Contains(e.NewItem))
                data.changes.Remove(e);
            original.UpdateWithIndex(e);
        });

        FilterDefinition<FTasks> filter = Builders<FTasks>
            .Filter
            .Eq(x => x.Id, data.Id);
        _taskService.UpdateMaterial(filter, original);

        return NoContent();
    }

    [HttpPatch("{overseer}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOverseer(
        TaskSingleUpdate<string> data)
    {
        FilterDefinition<Person> employeeFilter = Builders<Person>
            .Filter
            .Exists(x => x.Employed);
        List<string> employees = (await _peopleService
            .GetByAsync(employeeFilter))
            .Select(x => x.Id)
            .ToList();

        FTasks? original = await _taskService
            .GetOneByAsync(Filter.ById<FTasks>(data.Id));
        if (original is null) return NotFound();
        if (!employees.Contains(data.change)) return NotFound();

        if (!original.EmployeesAssigned.Contains(data.change))
        {
            UpdateList<string> newEmp = new()
                { Operation = 0, Key = 0, NewItem = data.change };
            original.EmployeesAssigned.UpdateWithIndex(newEmp);
            UpdateDefinition<Person> empUpdate = Builders<Person>
                .Update
                .Set(x => x.ModifiedAt, DateTime.Now)
                .AddToSet(x => x.Employed!.Oversees, data.Id);
            UpdateDto<Person> empUpdateWrapper =
                new(Filter.ById<Person>(data.Id), empUpdate);
            _peopleService.Update(empUpdateWrapper);
        }
        UpdateDefinition<FTasks> update = Builders<FTasks>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Set(x => x.Overseer, data.change);
        UpdateDto<FTasks> updateWrapper =
            new(Filter.ById<FTasks>(data.Id), update);

        _taskService.Update(updateWrapper);

        return NoContent();
    }

    [HttpPatch("{assigned}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAssigned(
        TaskSingleUpdateList<string> data)
    {
        FilterDefinition<Person> employeeFilter = Builders<Person>
            .Filter
            .Exists(x => x.Employed);
        List<string> employees = (await _peopleService
            .GetByAsync(employeeFilter))
            .Select(x => x.Id)
            .ToList();

        FTasks? original = await _taskService
            .GetOneByAsync(Filter.ById<FTasks>(data.Id));
        if (original is null) return NotFound();
        List<UpdateList<string>>? valid = FTaskService.ValidateAssigned(
            data.changes, employees, original.EmployeesAssigned);
        if (valid is null) return BadRequest();
        
        List<string> updated = original.EmployeesAssigned;
        valid.ForEach(updated.UpdateWithIndex);
        FilterDefinition<FTasks> filter = Builders<FTasks>
            .Filter
            .Eq(x => x.Id, data.Id);
        UpdateDefinition<FTasks> update = Builders<FTasks>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Set(x => x.EmployeesAssigned, updated);
        UpdateDto<FTasks> updateWrapper = new(filter, update);

        _taskService.Update(updateWrapper);
        
        return NoContent();
    }
}