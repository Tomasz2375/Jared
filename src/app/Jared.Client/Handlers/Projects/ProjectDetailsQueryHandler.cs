using System.Net.Http.Json;
using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using MediatR;

namespace Jared.Client.Handlers.Projects;

public class ProjectDetailsQueryHandler(HttpClient httpClient)
    : IRequestHandler<ProjectDetailsQuery, Result<ProjectDetailsDto>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<ProjectDetailsDto>> Handle(ProjectDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.PROJECTS}/{request.id}";

        var response = await httpClient.GetFromJsonAsync<Result<ProjectDetailsDto>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<ProjectDetailsDto>("Invalid response type");
        }

        return response;
    }
}
