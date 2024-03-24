using Jared.Application.Commands.EpicCommands;
using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Queries.EpicQueries;
using Jared.Domain.Abstractions;
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
    public async Task<Result<List<EpicListDto>>> GetEpicListAsync(int? projectId)
    {
        return await mediator.Send(new EpicListQuery(projectId));
    }

    [HttpPost]
    public async Task<Result> CreateEpic(EpicCreateCommand command)
    {
        return await mediator.Send(command);
    }
}
