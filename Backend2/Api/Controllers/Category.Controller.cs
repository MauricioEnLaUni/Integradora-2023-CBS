using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using System.Net.Mime;
using System.Security.Claims;
using Fictichos.Constructora.Utilities.MongoDB;
using Fictichos.Constructora.Auth;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly MaterialCategoryService _categoryService;
    private readonly UserService _userService;
    private readonly MaterialService _materialService;
    private readonly TokenService _tokenService;

    public CategoryController(
        MaterialCategoryService cat,
        UserService usr,
        MaterialService mat,
        TokenService token)
    {
        _categoryService = cat;
        _userService = usr;
        _materialService = mat;
        _tokenService = token;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        string header = HttpContext.Request.Headers["Authorization"]!;
        if (header is null) return Unauthorized();
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return BadRequest();

        List<MaterialCategory> raw = _categoryService.GetRoots();
        List<MaterialCategoryDto> output = new();
        raw.ForEach(e => {
            output.Add(e.To());
        });

        return Ok(output);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        string header = HttpContext.Request.Headers["Authorization"]!;
        if (header is null) return Unauthorized();
        IEnumerable<Claim> claims = _tokenService.GetClaimsFromHeader(header);
        string? idCredential = claims.Where(x => x.Type == "sub")
            .Select(x => x.Value)
            .SingleOrDefault();
        if (idCredential is null) return BadRequest();

        MaterialCategory? raw = _categoryService.GetCategory(id);
        if (raw is null) return NotFound();
        return Ok(raw.To());
    }

    [HttpPut]
    public async Task<IActionResult>
        Update([FromBody] UpdatedMatCategoryDto data)
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
            if (!roles.Contains("manager"))
            {
                return Forbid();
            }
        }

        UpdatedMatCategoryDto? sanitized = await _categoryService
            .ValidateUpdate(data);
        if (sanitized is null) return BadRequest();


        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        MaterialCategory? item = await _categoryService
            .GetOneByAsync(Filter.ById<MaterialCategory>(id));
        if (item is null) return NotFound();
        if (item.Children.Count > 0) return Conflict();
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
            if (!roles.Contains("manager"))
            {
                return Forbid();
            }
        }

        await _categoryService.DeleteOneAsync(id);
        return NoContent();
    }
}