using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Projects.Create;

public class ProjectCreateCommandHandler : IRequestHandler<ProjectCreateCommand, Result>
{
    private readonly HttpClient httpClient;

    public ProjectCreateCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.PROJECT_CREATE;

        var result = await httpClient.PostAsJsonAsync(baseUrl, request.dto, cancellationToken);

        return Result.Ok();
    }
}
