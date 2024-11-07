using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.Projects.List;

public class ProjectListQueryHandler(HttpClient httpClient)
    : IRequestHandler<ProjectListQuery, Result<List<ProjectListDto>>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<List<ProjectListDto>>> Handle(ProjectListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.PROJECT_LIST;

        var response = await httpClient.GetFromJsonAsync<Result<List<ProjectListDto>>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<List<ProjectListDto>>("Invalid response type");
        }

        return response;
    }
}
