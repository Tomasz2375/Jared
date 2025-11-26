using System.Net.Http.Json;
using System.Text;
using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using Jared.Dtos.Epics;
using MediatR;

namespace Jared.Client.Handlers.Epics;

public class EpicPageQueryHandler(HttpClient httpClient)
    : IRequestHandler<EpicPageQuery, Result<EpicPageDto>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<EpicPageDto>> Handle(EpicPageQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.EPIC_PAGE;
        string queryUrl = createQueryUrl(request);

        string url = baseUrl + queryUrl;

        var response = await httpClient.GetFromJsonAsync<Result<EpicPageDto>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<EpicPageDto>("Invalid response type");
        }

        return response;
    }

    private static string createQueryUrl(EpicPageQuery query)
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
