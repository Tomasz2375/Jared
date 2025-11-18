using Jared.Application.Requests.Epics.Create;
using Jared.Application.Requests.Epics.Details;
using Jared.Application.Requests.Epics.List;
using Jared.Application.Requests.Epics.Page;
using Jared.Application.Requests.Epics.Update;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Application.Controllers;

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
    [Route("List")]
    public async Task<Result<List<EpicListDto>>> EpicListAsync([FromQuery] int? projectId)
    {
        return await mediator.Send(new EpicListQuery(projectId));
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
