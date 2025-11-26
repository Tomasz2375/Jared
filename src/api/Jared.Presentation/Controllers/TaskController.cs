using Jared.Contracts.Tasks;
using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet("{id}")]
    public async Task<Result<TaskDetailsDto>> TaskDetailsAsync([FromRoute] int id)
    {
        return await mediator.Send(new TaskDetailsQuery(id));
    }

    [HttpGet("Page")]
    public async Task<Result<TaskPageDto>> TaskPageAsync(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? sortingProperty,
        [FromQuery] SortingDirection sortingDirection,
        [FromQuery] IDictionary<string, string?>? filter)
    {
        return await mediator.Send(new TaskPageQuery(
            page,
            pageSize,
            sortingProperty,
            sortingDirection,
            filter));
    }

    [Authorize]
    [HttpPut("Update")]
    public async Task<Result<bool>> TaskUpdateAsync([FromBody] TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskUpdateCommand(dto));
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<Result<bool>> TaskCreateAsync([FromBody] TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskCreateCommand(dto));
    }
}
