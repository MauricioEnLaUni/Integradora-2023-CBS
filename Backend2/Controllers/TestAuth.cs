using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Fictichos.Constructora.Controllers
{
    [ApiController]
    [Route("testAuth")]
    [Authorize]
    public class TestAuthController : ControllerBase
    {

    }
}