using Jared.Domain.Model;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.WorkLogDtos;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.WorkLogs.Statistics;

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
