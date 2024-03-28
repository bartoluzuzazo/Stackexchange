using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stackexchange.Application.TagServices.Queries;

namespace Stackexchange.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOk()
    {
        return Ok(Environment.GetEnvironmentVariable("SE_DB_CONNSTR"));
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetTagQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}