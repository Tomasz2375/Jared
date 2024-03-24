using Jared.Application.Commands.ProjectCommands;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Queries.ProjectQueries;
using Jared.Domain.Abstractions;
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
    public async Task<Result<List<ProjectListDto>>> GetProjectListAsync()
    {
        return await mediator.Send(new ProjectListQuery());
    }

    [HttpPost]
    public async Task<Result> CreateProject(ProjectCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
