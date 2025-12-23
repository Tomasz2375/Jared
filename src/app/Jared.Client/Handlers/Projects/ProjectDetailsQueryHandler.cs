using Jared.Client.Abstractions;
using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using MediatR;

namespace Jared.Client.Handlers.Projects;

public class ProjectDetailsQueryHandler(IApiClient apiClient)
    : IRequestHandler<ProjectDetailsQuery, Result<ProjectDetailsDto>>
{
    public async Task<Result<ProjectDetailsDto>> Handle(ProjectDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.PROJECTS}/{request.id}";

        return await apiClient.GetAsync<ProjectDetailsDto>(baseUrl, cancellationToken);
    }
}
