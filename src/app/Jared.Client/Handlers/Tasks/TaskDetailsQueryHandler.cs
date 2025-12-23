using Jared.Client.Abstractions;
using Jared.Client.Handlers;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Dtos.Tasks;
using MediatR;

namespace Jared.Client.Requests.Tasks;

public class TaskDetailsQueryHandler(IApiClient apiClient)
    : IRequestHandler<TaskDetailsQuery, Result<TaskDetailsDto>>
{
    public async Task<Result<TaskDetailsDto>> Handle(TaskDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.TASKS}/{request.id}";

        return await apiClient.GetAsync<TaskDetailsDto>(baseUrl, cancellationToken);
    }
}
