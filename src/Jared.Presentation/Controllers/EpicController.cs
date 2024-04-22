using Jared.Application.Commands.EpicCommands;
using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Queries.EpicQueries;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class EpicController
{
    private readonly IMediator mediator;

    public EpicController(IMediator mediator)
    {
        this.mediator = mediator;
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

    [HttpPost]
    public async Task<Result> EpicCreateAsync(EpicCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
