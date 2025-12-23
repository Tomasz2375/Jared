using System.Text;
using Jared.Client.Abstractions;
using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using MediatR;

namespace Jared.Client.Handlers.Projects;

public class ProjectPageQueryHandler(IApiClient apiClient)
    : IRequestHandler<ProjectPageQuery, Result<ProjectPageDto>>
{
    public async Task<Result<ProjectPageDto>> Handle(ProjectPageQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.PROJECTS;
        string queryUrl = createQueryUrl(request);
        string url = baseUrl + queryUrl;

        return await apiClient.GetAsync<ProjectPageDto>(url, cancellationToken);
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
