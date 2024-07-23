using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;
using System.Net.Http.Json;
using System.Text;

namespace Jared.Presentation.CQRS.Projects.Page;

public class ProjectPageQueryHandler : IRequestHandler<ProjectPageQuery, Result<ProjectPageDto>>
{
    private readonly HttpClient httpClient;

    public ProjectPageQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<ProjectPageDto>> Handle(ProjectPageQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = "project/page";
        string queryUrl = createQueryUrl(request.query);

        string url = baseUrl + queryUrl;

        return await httpClient.GetFromJsonAsync<Result<ProjectPageDto>>(url);
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
