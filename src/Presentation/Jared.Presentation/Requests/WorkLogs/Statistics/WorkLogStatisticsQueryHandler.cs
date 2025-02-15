using System.Net.Http.Json;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.WorkLogDtos;
using MediatR;

namespace Jared.Presentation.Requests.WorkLogs.Statistics;

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
        return $"?userId={query.userId}&mounth={query.month}&year={query.year}";
    }
}
