using Jared.Application.Commands.ProjectCommands;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Queries.ProjectQueries;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController
{
    private readonly IMediator mediator;

    public ProjectController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<Result<ProjectDetailsDto>> ProjectDetailsAsync([FromRoute] int id)
    {
        return await mediator.Send(new ProjectDetailsQuery(id));
    }

    [HttpGet("List")]
    public async Task<Result<List<ProjectListDto>>> ProjectListAsync()
    {
        return await mediator.Send(new ProjectListQuery());
    }

    [HttpGet("Page")]
    public async Task<Result<ProjectPageDto>> ProjectPageAsync(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? sortingProperty,
        [FromQuery] SortingDirection sortingDirection,
        [FromQuery] IDictionary<string, string?>? filter)
    {
        return await mediator.Send(new ProjectPageQuery(
            page,
            pageSize,
            sortingProperty,
            sortingDirection,
            filter));
    }

    [HttpPut("Update")]
    public async Task<Result> ProjectUpdateAsync([FromBody] ProjectDetailsDto dto)
    {
        return await mediator.Send(new ProjectUpdateCommand(dto));
    }

    [HttpPost("Create")]
    public async Task<Result> ProjectCreateAsync([FromBody] ProjectDetailsDto dto)
    {
        return await mediator.Send(new ProjectCreateCommand(dto));
    }
}
