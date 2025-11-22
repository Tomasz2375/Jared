using System.Net.Http.Json;
using Jared.Contracts.Worklogs;
using Jared.Dtos.WorkLogs;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Handlers.WorkLogs;

public class WorkLogStatisticsQueryHandler(HttpClient httpClient)
    : IRequestHandler<WorkLogStatisticsQuery, Result<List<WorkLogStatisticsDto>>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<List<WorkLogStatisticsDto>>> Handle(WorkLogStatisticsQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.WORK_LOG_STATISTICS;
        var queryUrl = createQueryUrl(request);
        var url = baseUrl + queryUrl;

        var response = await httpClient
            .GetFromJsonAsync<Result<List<WorkLogStatisticsDto>>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<List<WorkLogStatisticsDto>>("Invalid response type");
        }

        return response;
    }

    private static string createQueryUrl(WorkLogStatisticsQuery query)
    {
        return $"?userId={query.userId}&month={query.month}&year={query.year}";
    }
}
