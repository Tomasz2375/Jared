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

        var tt = result.StatusCode;

        return Result.Ok();
    }
}
