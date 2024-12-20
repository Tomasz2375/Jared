﻿using Jared.Application.Requests.Tasks.Create;
using Jared.Application.Requests.Tasks.Details;
using Jared.Application.Requests.Tasks.List;
using Jared.Application.Requests.Tasks.Page;
using Jared.Application.Requests.Tasks.Update;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet("{id}")]
    public async Task<Result<TaskDetailsDto>> TaskDetailsAsync([FromRoute] int id)
    {
        return await mediator.Send(new TaskDetailsQuery(id));
    }

    [HttpGet("List")]
    public async Task<Result<List<TaskListDto>>> TaskListAsync([FromQuery] int? projectId, [FromQuery] int? epicId)
    {
        return await mediator.Send(new TaskListQuery(projectId, epicId));
    }

    [HttpGet("Page")]
    public async Task<Result<TaskPageDto>> TaskPageAsync(
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

    [Authorize]
    [HttpPut("Update")]
    public async Task<Result<bool>> TaskUpdateAsync([FromBody] TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskUpdateCommand(dto));
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<Result<bool>> TaskCreateAsync([FromBody] TaskDetailsDto dto)
    {
        return await mediator.Send(new TaskCreateCommand(dto));
    }
}
