using Microsoft.AspNetCore.Mvc;

namespace Stackexchange.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOk()
    {
        return Ok();
    }
}