using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Projects.Update;

public class ProjectUpdateCommandHandler : IRequestHandler<ProjectUpdateCommand, Result>
{
    private readonly HttpClient httpClient;

    public ProjectUpdateCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result> Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
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
