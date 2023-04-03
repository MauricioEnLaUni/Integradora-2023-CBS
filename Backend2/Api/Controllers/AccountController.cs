using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly ProjectService _projectService;

    public AccountController(
        AccountService accounts,
        ProjectService projects)
    {
        _accountService = accounts;
        _projectService = projects;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] NewAccountDto data)
    {
        List<Account> accounts = await _accountService
            .GetByAsync(Filter.Empty<Account>());
        Project? owner = await _projectService
            .GetOneByAsync(Filter.ById<Project>(data.Owner));
        HTTPResult<NewAccountDto> validation = _accountService
            .ValidateNew(accounts, data, owner);
        if (validation.Code == 404) return NotFound();
        if (validation.Code == 409) return Conflict();

        Account newItem = _accountService.InsertOne(validation.Value!);
        UpdateDefinition<Project> projectUpdate = Builders<Project>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Set(x => x.PayHistory, newItem.Id);
        UpdateDto<Project> wrapper =
            new(Filter.ById<Project>(owner!.Id), projectUpdate);
        _projectService.Update(wrapper);
        
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = newItem!.Id },
            value: newItem.ToDto());
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        List<Account> raw = _accountService.GetBy(Filter.Empty<Account>());
        List<AccountDto> result = new();
        raw.ForEach(e => {
            result.Add(e.ToDto());
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(string id)
    {
        Account? account = _accountService.GetOneBy(Filter.ById<Account>(id));
        if (account is null) return NotFound();
        return Ok(account.ToDto());
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteById(string id)
    {
        _accountService.DeleteOne(Filter.ById<Account>(id));
        return NoContent();
    }

    [HttpDelete("payments")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePayments(string id)
    {
        Account? item = _accountService.GetOneBy(Filter.ById<Account>(id));
        if (item is null) return NotFound();

        item.ClearPayments();
        _accountService.ReplaceOne(Filter.ById<Account>(id), item);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromBody] UpdatedAccountDto data)
    {
        List<Account> accounts = await _accountService
            .GetByAsync(Filter.Empty<Account>());
        Project? owner = await _projectService
            .GetOneByAsync(Filter.ById<Project>(data.Id));
        Account? original = accounts
            .Where(x => x.Id == data.Id)
            .SingleOrDefault();
        if (owner is null || original is null) return NotFound();

        UpdatedAccountDto valid = _accountService
            .ValidateUpdate(data, accounts, owner);
        original.Update(valid);
        _accountService.ReplaceOne(Filter.ById<Account>(original.Id), original);

        return NoContent();
    }

}