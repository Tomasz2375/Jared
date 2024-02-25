using Jared.Application.Commands.Task;
using Jared.Domain.Abstractions;
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
}
