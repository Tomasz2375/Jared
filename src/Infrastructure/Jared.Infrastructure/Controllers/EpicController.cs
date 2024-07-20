using Jared.Application.Commands.EpicCommands;
using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Queries.EpicQueries;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EpicController
{
    private readonly IMediator mediator;

    public EpicController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<Result<EpicDetailsDto>> EpicDetailsAsync(int id)
    {
        return await mediator.Send(new EpicDetailsQuery(id));
    }

    [HttpGet]
    [Route("List")]
    public async Task<Result<List<EpicListDto>>> EpicListAsync(int? projectId)
    {
        return await mediator.Send(new EpicListQuery(projectId));
    }

    [HttpGet]
    [Route("Page")]
    public async Task<Result<EpicPageDto>> EpicPageAsync(
        int page,
        int pageSize,
        string? sortingProperty,
        SortingDirection sortingDirection,
        IDictionary<string, string?>? filter)
    {
        return await mediator.Send(new EpicPageQuery(
            page,
            pageSize,
            sortingProperty,
            sortingDirection,
            filter));
    }

    [HttpPut("{id}")]
    public async Task<Result> EpicUpdateAsync(EpicDetailsDto dto)
    {
        return await mediator.Send(new EpicUpdateCommand(dto));
    }

    [HttpPost]
    public async Task<Result> EpicCreateAsync(EpicCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
