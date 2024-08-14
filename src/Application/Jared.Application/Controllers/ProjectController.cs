using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Requests.Projects.Create;
using Jared.Application.Requests.Projects.Details;
using Jared.Application.Requests.Projects.List;
using Jared.Application.Requests.Projects.Page;
using Jared.Application.Requests.Projects.Update;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
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
    public async Task<Result<bool>> ProjectUpdateAsync([FromBody] ProjectDetailsDto dto)
    {
        return await mediator.Send(new ProjectUpdateCommand(dto));
    }

    [HttpPost("Create")]
    public async Task<Result<bool>> ProjectCreateAsync([FromBody] ProjectDetailsDto dto)
    {
        return await mediator.Send(new ProjectCreateCommand(dto));
    }
}
