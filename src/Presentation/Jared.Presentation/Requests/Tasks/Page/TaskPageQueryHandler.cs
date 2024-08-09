using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;
using System.Net.Http.Json;
using System.Text;

namespace Jared.Presentation.Requests.Tasks.Page;

public class TaskPageQueryHandler : IRequestHandler<TaskPageQuery, Result<TaskPageDto>>
{
    private readonly HttpClient httpClient;

    public TaskPageQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<TaskPageDto>> Handle(TaskPageQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.TASK_PAGE;
        string queryUrl = createQueryUrl(request.query);

        string url = baseUrl + queryUrl;

        var response = await httpClient.GetFromJsonAsync<Result<TaskPageDto>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<TaskPageDto>("Invalid response type");
        }

        return response;
    }

    private string createQueryUrl(Query query)
    {
        StringBuilder queryBuilder = new();

        queryBuilder.Append("?page=");
        queryBuilder.Append(query.Page);
        queryBuilder.Append("&pageSize=");
        queryBuilder.Append(query.PageSize);
        if (query.SortingProperty is not null)
        {
            queryBuilder.Append("&sortingProperty=");
            queryBuilder.Append(query.SortingProperty);
        }
        if (query.SortingDirection is not null)
        {
            queryBuilder.Append("&sortingDirection=");
            queryBuilder.Append(query.SortingDirection);
        }
        if (query.Filter is not null)
        {
            var filters = query.Filter.Select(x => "&" + x.Key + "=" + x.Value);
            queryBuilder.Append(string.Join("", filters));
        }

        return queryBuilder.ToString();
    }
}
