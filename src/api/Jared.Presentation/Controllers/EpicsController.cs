using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Dtos.Epics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EpicsController(IMediator mediator)
{
    [HttpGet("{id}")]
    public async Task<Result<EpicDetailsDto>> GetById([FromRoute] int id)
    {
        return await mediator.Send(new EpicDetailsQuery(id));
    }

    [HttpGet]
    public async Task<Result<EpicPageDto>> GetAll(
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

    [HttpPut("{Id}")]
    public async Task<Result<bool>> Update([FromRoute] int id, [FromBody] EpicDetailsDto dto)
    {
        return await mediator.Send(new EpicUpdateCommand(dto));
    }

    [HttpPost]
    public async Task<Result<bool>> Create([FromBody] EpicDetailsDto dto)
    {
        return await mediator.Send(new EpicCreateCommand(dto));
    }
}
