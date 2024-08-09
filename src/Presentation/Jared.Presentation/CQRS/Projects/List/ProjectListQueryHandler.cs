using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Projects.List;

public class ProjectListQueryHandler : IRequestHandler<ProjectListQuery, Result<List<ProjectListDto>>>
{
    private readonly HttpClient httpClient;

    public ProjectListQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

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
