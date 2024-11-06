using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.Epics.List;

public class EpicListQueryHandler(HttpClient httpClient)
    : IRequestHandler<EpicListQuery, Result<List<EpicListDto>>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<List<EpicListDto>>> Handle(EpicListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.EPIC_LIST;
        string queryUrl = request.projectId is null
            ? string.Empty
            : $"?projectId={request.projectId}";
        string url = baseUrl + queryUrl;

        var response = await httpClient.GetFromJsonAsync<Result<List<EpicListDto>>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<List<EpicListDto>>("Invalid response type");
        }

        return response;
    }
}
