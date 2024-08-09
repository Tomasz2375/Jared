using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.Epics.Details;

public class EpicDetailsQueryHandler : IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>
{
    private readonly HttpClient httpClient;

    public EpicDetailsQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

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
