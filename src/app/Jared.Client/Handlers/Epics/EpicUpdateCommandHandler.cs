using System.Net.Http.Json;
using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using MediatR;

namespace Jared.Client.Handlers.Epics;

public class EpicUpdateCommandHandler(HttpClient httpClient)
    : IRequestHandler<EpicUpdateCommand, Result<bool>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<bool>> Handle(EpicUpdateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.EPICS}/{request.dto.Id}";

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
