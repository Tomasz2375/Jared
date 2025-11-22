using Jared.Contracts.Worklogs;
using Jared.Dtos.WorkLogs;
using Jared.Shared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkLogController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet("Statistics")]
    public async Task<Result<List<WorkLogStatisticsDto>>> TaskListAsync(
        [FromQuery] int userId,
        [FromQuery] int month,
        [FromQuery] int year)
    {
        return await mediator.Send(new WorkLogStatisticsQuery(userId, month, year));
    }
}
