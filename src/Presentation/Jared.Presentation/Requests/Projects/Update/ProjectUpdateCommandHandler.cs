using Jared.Shared.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.Projects.Update;

public class ProjectUpdateCommandHandler(HttpClient httpClient)
    : IRequestHandler<ProjectUpdateCommand, Result<bool>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<bool>> Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.PROJECT_UPDATE;

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
