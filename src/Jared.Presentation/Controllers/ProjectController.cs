using Jared.Application.Commands.ProjectCommands;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Queries.ProjectQueries;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController
{
    private readonly IMediator mediator;

    public ProjectController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Route("List")]
    public async Task<Result<List<ProjectListDto>>> GetProjectListAsync()
    {
        return await mediator.Send(new ProjectListQuery());
    }

    [HttpGet]
    [Route("Page")]
    public async Task<Result<ProjectPageDto>> GetProjectPageAsync(
        int page,
        int pageSize,
        string? filter,
        string? sortingProperty,
        SortingDirection sortingDirection)
    {
        return await mediator.Send(new ProjectPageQuery(
            page,
            pageSize,
            filter,
            sortingProperty,
            sortingDirection));
    }

    [HttpPost]
    public async Task<Result> CreateProject(ProjectCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
