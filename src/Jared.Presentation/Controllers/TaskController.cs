using Jared.Application.Commands.Task;
using Jared.Application.Dtos.TaskDto;
using Jared.Application.Queries.TaskQueries;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("controller")]
public class TaskController : ControllerBase
{
    private readonly IMediator mediator;

    public TaskController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<Result> CreateTask(CreateTaskCommand command)
    {
        await mediator.Send(command);

        return Result.Ok();
    }

    [HttpGet]
    public async Task<Result<TaskPageDto>> GetTaskListAsync(
        int page,
        int pageSize,
        string? filter,
        string? sortingProperty,
        SortingDirection sortingDirection)
    {
        var result = await mediator.Send(new TaskListQuery(
            page,
            pageSize,
            filter,
            sortingProperty,
            sortingDirection));

        return result;
    }
}
