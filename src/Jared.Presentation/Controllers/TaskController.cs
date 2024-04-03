using Jared.Application.Commands.TaskCommand;
using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Queries.TaskQueries;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator mediator;

    public TaskController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Route("Page")]
    public async Task<Result<TaskPageDto>> GetTaskPageAsync(
        int page,
        int pageSize,
        string? filter,
        string? sortingProperty,
        SortingDirection sortingDirection)
    {
        return await mediator.Send(new TaskPageQuery(
            page,
            pageSize,
            filter,
            sortingProperty,
            sortingDirection));
    }

    [HttpPost]
    public async Task<Result> CreateTask(TaskCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
