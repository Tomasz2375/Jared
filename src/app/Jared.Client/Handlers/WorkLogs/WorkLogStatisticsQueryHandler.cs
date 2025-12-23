using Jared.Client.Abstractions;
using Jared.Contracts.Worklogs;
using Jared.Core.Abstractions;
using Jared.Dtos.WorkLogs;
using MediatR;

namespace Jared.Client.Handlers.WorkLogs;

public class WorkLogStatisticsQueryHandler(IApiClient apiClient)
    : IRequestHandler<WorkLogStatisticsQuery, Result<List<WorkLogStatisticsDto>>>
{
    public async Task<Result<List<WorkLogStatisticsDto>>> Handle(WorkLogStatisticsQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.WORK_LOGS;
        var queryUrl = createQueryUrl(request);
        var url = baseUrl + queryUrl;

        return await apiClient.GetAsync<List<WorkLogStatisticsDto>>(url, cancellationToken);
    }

    private static string createQueryUrl(WorkLogStatisticsQuery query)
    {
        return $"?userId={query.userId}&month={query.month}&year={query.year}";
    }
}
