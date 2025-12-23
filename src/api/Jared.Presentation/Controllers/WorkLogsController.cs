using Jared.Contracts.Worklogs;
using Jared.Core.Abstractions;
using Jared.Dtos.WorkLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WorkLogsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Result<List<WorkLogStatisticsDto>>> GetAll(
        [FromQuery] int userId,
        [FromQuery] int month,
        [FromQuery] int year)
    {
        return await mediator.Send(new WorkLogStatisticsQuery(userId, month, year));
    }
}
