using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Dtos.Epics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EpicController(IMediator mediator)
{
    private readonly IMediator mediator = mediator;

    [HttpGet("{id}")]
    public async Task<Result<EpicDetailsDto>> EpicDetailsAsync([FromRoute] int id)
    {
        return await mediator.Send(new EpicDetailsQuery(id));
    }

    [HttpGet]
    [Route("Page")]
    public async Task<Result<EpicPageDto>> EpicPageAsync(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? sortingProperty,
        [FromQuery] SortingDirection sortingDirection,
        [FromQuery] IDictionary<string, string?>? filter)
    {
        return await mediator.Send(new EpicPageQuery(
            page,
            pageSize,
            sortingProperty,
            sortingDirection,
            filter));
    }

    [HttpPut("Update")]
    public async Task<Result<bool>> EpicUpdateAsync([FromBody] EpicDetailsDto dto)
    {
        return await mediator.Send(new EpicUpdateCommand(dto));
    }

    [HttpPost("Create")]
    public async Task<Result<bool>> EpicCreateAsync([FromBody] EpicDetailsDto dto)
    {
        return await mediator.Send(new EpicCreateCommand(dto));
    }
}
