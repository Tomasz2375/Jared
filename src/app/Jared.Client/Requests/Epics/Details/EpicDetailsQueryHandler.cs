using System.Net.Http.Json;
using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Epics.Details;

public class EpicDetailsQueryHandler(HttpClient httpClient)
    : IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<EpicDetailsDto>> Handle(EpicDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.EPIC_DETAILS}/{request.id}";

        var response = await httpClient.GetFromJsonAsync<Result<EpicDetailsDto>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<EpicDetailsDto>("Invalid response type");
        }

        return response;
    }
}
