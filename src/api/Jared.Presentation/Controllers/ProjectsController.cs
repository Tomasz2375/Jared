using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Dtos.Projects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IMediator mediator)
{
    [HttpGet("{id}")]
    public async Task<Result<ProjectDetailsDto>> GetById([FromRoute] int id)
    {
        return await mediator.Send(new ProjectDetailsQuery(id));
    }

    [HttpGet]
    public async Task<Result<ProjectPageDto>> GetAll(
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

    [HttpPut("{Id}")]
    public async Task<Result<bool>> Update([FromRoute] int id, [FromBody] ProjectDetailsDto dto)
    {
        return await mediator.Send(new ProjectUpdateCommand(dto));
    }

    [HttpPost]
    public async Task<Result<bool>> Create([FromBody] ProjectDetailsDto dto)
    {
        return await mediator.Send(new ProjectCreateCommand(dto));
    }
}
