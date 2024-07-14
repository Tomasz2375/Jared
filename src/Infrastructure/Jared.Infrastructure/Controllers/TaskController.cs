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
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator mediator;

    public TaskController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<Result<TaskDetailsDto>> TaskDetailsAsync(int id)
    {
        return await mediator.Send(new TaskDetailsQuery(id));
    }

    [HttpGet]
    [Route("List")]
    public async Task<Result<List<TaskListDto>>> TaskListAsync(int? projectId, int? epicId)
    {
        return await mediator.Send(new TaskListQuery(projectId, epicId));
    }

    [HttpGet]
    [Route("Page")]
    public async Task<Result<TaskPageDto>> TaskPageAsync(
        int page,
        int pageSize,
        string? sortingProperty,
        SortingDirection sortingDirection,
        IDictionary<string, string?>? filter)
    {
        return await mediator.Send(new TaskPageQuery(
            page,
            pageSize,
            sortingProperty,
            sortingDirection,
            filter));
    }

    [HttpPut("{id}")]
    public async Task<Result> TaskUpdateAsync(TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskUpdateCommand(dto));
    }

    [HttpPost]
    public async Task<Result> TaskCreateAsync(TaskCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
