using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Epics.List;

public class EpicListQueryHandler : IRequestHandler<EpicListQuery, Result<List<EpicListDto>>>
{
    private readonly HttpClient httpClient;

    public EpicListQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<List<EpicListDto>>> Handle(EpicListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.EPIC_LIST;
        string queryUrl = request.projectId is null
            ? string.Empty
            : $"?projectId={request.projectId}";
        string url = baseUrl + queryUrl;
        return await httpClient.GetFromJsonAsync<Result<List<EpicListDto>>>(url);
    }
}
