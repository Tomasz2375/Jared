using System.Net.Http.Json;
using System.Text;
using Jared.Client.ColumnDefinitions;
using Jared.Contracts.Projects;
using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Handlers.Projects;

public class ProjectPageQueryHandler(HttpClient httpClient)
    : IRequestHandler<ProjectPageQuery, Result<ProjectPageDto>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<ProjectPageDto>> Handle(ProjectPageQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.PROJECT_PAGE;
        string queryUrl = createQueryUrl(request);

        string url = baseUrl + queryUrl;

        var response = await httpClient.GetFromJsonAsync<Result<ProjectPageDto>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<ProjectPageDto>("Invalid response type");
        }

        return response;
    }

    private static string createQueryUrl(ProjectPageQuery query)
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
