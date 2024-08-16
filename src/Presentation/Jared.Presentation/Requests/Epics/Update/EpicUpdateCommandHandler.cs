using Jared.Shared.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.Epics.Update;

public class EpicUpdateCommandHandler : IRequestHandler<EpicUpdateCommand, Result<bool>>
{
    private readonly HttpClient httpClient;

    public EpicUpdateCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<bool>> Handle(EpicUpdateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.EPIC_UPDATE;

        var result = await httpClient.PutAsJsonAsync(baseUrl, request.dto, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            return Result.Fail<bool>($"Something went wrong. Status code: {(int)result.StatusCode} ({result.StatusCode})");
        }

        var response = await result.Content.ReadFromJsonAsync<Result<bool>>();

        if (response is null)
        {
            return Result.Fail<bool>("Invalid response type");
        }

        return response;
    }
}
