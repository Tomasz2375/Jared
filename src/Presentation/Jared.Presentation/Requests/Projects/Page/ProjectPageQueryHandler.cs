﻿using Jared.Presentation.ColumnDefinitions;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using MediatR;
using System.Net.Http.Json;
using System.Text;

namespace Jared.Presentation.Requests.Projects.Page;

public class ProjectPageQueryHandler(HttpClient httpClient)
    : IRequestHandler<ProjectPageQuery, Result<ProjectPageDto>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<ProjectPageDto>> Handle(ProjectPageQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.PROJECT_PAGE;
        string queryUrl = createQueryUrl(request.query);

        string url = baseUrl + queryUrl;

        var response = await httpClient.GetFromJsonAsync<Result<ProjectPageDto>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<ProjectPageDto>("Invalid response type");
        }

        return response;
    }

    private static string createQueryUrl(Query query)
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
            queryBuilder.Append(string.Join(string.Empty, filters));
        }

        return queryBuilder.ToString();
    }
}
