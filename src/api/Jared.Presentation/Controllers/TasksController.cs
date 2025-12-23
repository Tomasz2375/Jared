using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Dtos.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<Result<TaskDetailsDto>> GetById([FromRoute] int id)
    {
        return await mediator.Send(new TaskDetailsQuery(id));
    }

    [HttpGet]
    public async Task<Result<TaskPageDto>> GetAll(
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

    [HttpPut("{Id}")]
    public async Task<Result<bool>> Update([FromRoute] int id, [FromBody] TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskUpdateCommand(dto));
    }

    [HttpPost]
    public async Task<Result<bool>> Create([FromBody] TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskCreateCommand(dto));
    }
}
