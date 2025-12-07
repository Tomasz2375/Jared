using Jared.Contracts.Worklogs;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Dtos.WorkLogs;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.WorkLogs;

public class WorkLogStatisticsQueryHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<WorkLogStatisticsQuery, Result<List<WorkLogStatisticsDto>>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<List<WorkLogStatisticsDto>>> Handle(
        WorkLogStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var workLogQuery = dataContext
                .Set<WorkLog>()
                .Include(x => x.Task)
                .ThenInclude(x => x!.Project)
                .Where(x =>
                    x.UserId == request.userId &&
                    x.WorkDate.Month == request.month &&
                    x.WorkDate.Year == request.year)
                .AsNoTracking();

            var workLog = await workLogQuery.ToListAsync(cancellationToken);

            var result = mapper.Map<List<WorkLogStatisticsDto>>(workLog);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<WorkLogStatisticsDto>>(ex.Message);
        }
    }
}
