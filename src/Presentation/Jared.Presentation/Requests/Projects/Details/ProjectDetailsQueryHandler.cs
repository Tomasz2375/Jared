using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.Projects.Details;

public class ProjectDetailsQueryHandler : IRequestHandler<ProjectDetailsQuery, Result<ProjectDetailsDto>>
{
    private readonly HttpClient httpClient;

    public ProjectDetailsQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<ProjectDetailsDto>> Handle(ProjectDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.PROJECT_DETAILS}/{request.id}";

        var response = await httpClient.GetFromJsonAsync<Result<ProjectDetailsDto>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<ProjectDetailsDto>("Invalid response type");
        }

        return response;
    }
}
