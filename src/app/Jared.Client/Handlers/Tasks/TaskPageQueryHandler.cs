using System.Net.Http.Json;
using System.Text;
using Jared.Client.Handlers;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Dtos.Tasks;
using MediatR;

namespace Jared.Client.Requests.Tasks;

public class TaskPageQueryHandler(HttpClient httpClient)
    : IRequestHandler<TaskPageQuery, Result<TaskPageDto>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<TaskPageDto>> Handle(TaskPageQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.TASKS;
        string queryUrl = createQueryUrl(request);

        string url = baseUrl + queryUrl;

        var response = await httpClient.GetFromJsonAsync<Result<TaskPageDto>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<TaskPageDto>("Invalid response type");
        }

        return response;
    }

    private static string createQueryUrl(TaskPageQuery query)
    {
        StringBuilder queryBuilder = new();

        queryBuilder.Append("?page=");
        queryBuilder.Append(query.page);
        queryBuilder.Append("&pageSize=");
        queryBuilder.Append(query.pageSize);
        if (query.sortingProperty is not null)
        {
            queryBuilder.Append("&sortingProperty=");
            queryBuilder.Append(query.sortingProperty);
        }

        if (query.sortingDirection is not null)
        {
            queryBuilder.Append("&sortingDirection=");
            queryBuilder.Append(query.sortingDirection);
        }

        if (query.filters is not null)
        {
            var filters = query.filters.Select(x => "&" + x.Key + "=" + x.Value);
            queryBuilder.Append(string.Join(string.Empty, filters));
        }

        return queryBuilder.ToString();
    }
}
