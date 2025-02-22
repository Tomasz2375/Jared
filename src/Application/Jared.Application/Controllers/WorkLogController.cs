using Jared.Application.Requests.WorkLogs.Statistics;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.WorkLogDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Application.Controllers;

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
