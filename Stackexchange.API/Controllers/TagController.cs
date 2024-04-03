using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stackexchange.Application.DTOs.Tag;
using Stackexchange.Application.TagServices.Commands;
using Stackexchange.Application.TagServices.Queries;
using Stackexchange.Domain.Tags;

namespace Stackexchange.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController(IConfiguration configuration, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPage(int page, int count, string sortField, bool asc)
    {
        var sortFields = new List<string> { "none", "name", "percentage" };
        if (!sortFields.Contains(sortField)) return BadRequest();
        var countLimit = int.Parse(configuration["GET_TAG_PAGE_LIMIT"] ?? "100");
        if (count > countLimit) return BadRequest($"Count cannot be higher than {countLimit}");
        var query = new GetTagPageQuery(page, count, sortField, asc);
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var command = new PostTagCommand();
        await mediator.Send(command);
        return CreatedAtAction(nameof(GetPage), "");
    }
}