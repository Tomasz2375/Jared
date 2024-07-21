using Jared.Application.Commands.TaskCommand;
using Jared.Application.Commands.TaskCommands;
using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Queries.TaskQueries;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator mediator;

    public TaskController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<Result<TaskDetailsDto>> TaskDetailsAsync([FromRoute] int id)
    {
        return await mediator.Send(new TaskDetailsQuery(id));
    }

    [HttpGet("List")]
    public async Task<Result<List<TaskListDto>>> TaskListAsync([FromQuery] int? projectId, [FromQuery] int? epicId)
    {
        return await mediator.Send(new TaskListQuery(projectId, epicId));
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

    [HttpPut("{id}")]
    public async Task<Result> TaskUpdateAsync([FromBody] TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskUpdateCommand(dto));
    }

    [HttpPost("Create")]
    public async Task<Result> TaskCreateAsync([FromBody] TaskCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
