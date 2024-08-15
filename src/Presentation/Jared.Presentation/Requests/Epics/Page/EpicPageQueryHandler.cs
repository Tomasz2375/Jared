using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;
using System.Net.Http.Json;
using System.Text;

namespace Jared.Presentation.Requests.Epics.Page;

public class EpicPageQueryHandler : IRequestHandler<EpicPageQuery, Result<EpicPageDto>>
{
    private readonly HttpClient httpClient;

    public EpicPageQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<EpicPageDto>> Handle(EpicPageQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.EPIC_PAGE;
        string queryUrl = createQueryUrl(request.query);

        string url = baseUrl + queryUrl;

        var response = await httpClient.GetFromJsonAsync<Result<EpicPageDto>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<EpicPageDto>("Invalid response type");
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
