using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stackexchange.Application.DTOs.Tag;
using Stackexchange.Application.TagServices.Commands;
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
    public async Task<IActionResult> GetPage(int page, int count, string sortField, bool asc)
    {
        var sortFields = new List<string> { "none", "name", "percentage" };
        if (!sortFields.Contains(sortField)) return BadRequest();
        var query = new GetTagPageQuery(page, count, sortField, asc);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetTagQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TagPost request)
    {
        var command = new PostTagCommand(request.RedownloadAll);
        await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), "");
    }
}