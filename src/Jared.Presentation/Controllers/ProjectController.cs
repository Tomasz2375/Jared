﻿using Jared.Application.Commands.ProjectCommands;
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
    public async Task<Result<List<ProjectListDto>>> ProjectListAsync()
    {
        return await mediator.Send(new ProjectListQuery());
    }

    [HttpGet]
    [Route("Page")]
    public async Task<Result<ProjectPageDto>> ProjectPageAsync(
        int page,
        int pageSize,
        string? sortingProperty,
        SortingDirection sortingDirection,
        IDictionary<string, string?>? filter)
    {
        return await mediator.Send(new ProjectPageQuery(
            page,
            pageSize,
            sortingProperty,
            sortingDirection,
            filter));
    }

    [HttpPost]
    public async Task<Result> ProjectCreateAsync(ProjectCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
