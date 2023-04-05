using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Abstraction;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Middleware;
using System.Security.Claims;
using Fictichos.Constructora.Utilities.MongoDB;
using Fictichos.Constructora.Auth;

namespace Fictichos.Constructora.Controllers;

[ApiController]
[Route("[controller]")]
public class RefreshController : ControllerBase
{
    private readonly IJwtProvider _jwtProvider;
    private readonly TokenService _tokenService;

    public RefreshController(
        TokenService tokens,
        IJwtProvider jwtProvider)
    {
        _tokenService = tokens;
        _jwtProvider = jwtProvider;
    }

    [HttpGet]
    public IActionResult RefreshToken()
    {
        return Ok();
    }
}