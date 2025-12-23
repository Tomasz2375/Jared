using Jared.Client.Abstractions;
using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using MediatR;

namespace Jared.Client.Handlers.Projects;

public class ProjectUpdateCommandHandler(IApiClient apiClient)
    : IRequestHandler<ProjectUpdateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.PROJECTS}/{request.dto.Id}";

        return await apiClient.PutAsync<ProjectDetailsDto, bool>(baseUrl, request.dto, cancellationToken);
    }
}
