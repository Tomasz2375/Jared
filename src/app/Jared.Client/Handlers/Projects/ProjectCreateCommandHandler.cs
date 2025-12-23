using Jared.Client.Abstractions;
using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using MediatR;

namespace Jared.Client.Handlers.Projects;

public class ProjectCreateCommandHandler(IApiClient apiClient)
    : IRequestHandler<ProjectCreateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.PROJECTS;

        return await apiClient.PostAsync<ProjectDetailsDto, bool>(baseUrl, request.dto, cancellationToken);
    }
}
