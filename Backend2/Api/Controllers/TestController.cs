using Fictichos.Constructora.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Fictichos.Constructora.Repository;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    public TestController() {}

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllTypes()
    {
        SerializerTest ex = new SerializerTest();
        return Ok(ex);
    }
}