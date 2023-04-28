using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities.MongoDB;
using Fictichos.Constructora.Auth;
using System.Security.Claims;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly ProjectService _projectService;
    private readonly TokenService _tokenService;
    private readonly UserService _userService;

    public AccountController(
        AccountService accounts,
        ProjectService projects,
        TokenService token,
        UserService user)
    {
        _accountService = accounts;
        _projectService = projects;
        _tokenService = token;
        _userService = user;
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
        string header = HttpContext.Request.Headers["Authorization"]!;
        if (header is null) return Unauthorized();
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return BadRequest();

        User? usr = _userService.GetOneBy(Filter.ById<User>(idCredential));
        if (usr is null) return NotFound();
        if (!usr.Active) return Forbid();

        if (!usr.IsAdmin())
        {
            List<string> roles = usr
                .Credentials
                .Where(x => x.Type == "role")
                .Select(x => x.Value)
                .ToList();
            if (!roles.Contains("sales") && !roles.Contains("overseer") && !roles.Contains("manager"))
            {
                return Forbid();
            }
        }
        
        List<Account> raw = _accountService.GetBy(Filter.Empty<Account>());
        List<AccountDto> result = new();
        raw.ForEach(e => {
            result.Add(e.ToDto());
        });
        return Ok(result);
    }

    [HttpPost("projects")]
    public IActionResult AllFromProject(
        [FromBody] List<string> ids)
    {
        string? token = Request.Cookies["fid"];
        if (token is null) return Unauthorized();
        string? sub = _tokenService.CookieAuth(token);
        if (sub is null) return Forbid();
        
        User? auth = _userService
            .AuthRoles(sub, null, new() { "user" } );
        if (auth is null) return Forbid();

        FilterDefinition<Account> filter = Builders<Account>
            .Filter
            .In(x => x.Owner, ids);
        List<Account> accounts = _accountService.GetBy(filter);
        List<AccountDto> result = new();
        accounts.ForEach(e => result.Add(e.ToDto()));
        
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