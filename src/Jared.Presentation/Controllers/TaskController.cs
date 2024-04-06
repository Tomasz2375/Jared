using Jared.Application.Commands.TaskCommand;
using Jared.Application.Commands.TaskCommands;
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

    [HttpGet("{id}")]
    public async Task<Result<TaskDetailsDto>> TaskDetails(int id)
    {
        return await mediator.Send(new TaskDetailsQuery(id));
    }

    [HttpGet]
    [Route("Page")]
    public async Task<Result<TaskPageDto>> TaskPage(
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

    [HttpPut("{id}")]
    public async Task<Result> TaskUpdate(TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskUpdateCommand(dto));
    }

    [HttpPost]
    public async Task<Result> TaskCreate(TaskCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
