using Jared.Client.Abstractions;
using Jared.Client.Handlers;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Dtos.Tasks;
using MediatR;

namespace Jared.Client.Requests.Tasks;

public class TaskUpdateCommandHandler(IApiClient apiClient)
    : IRequestHandler<TaskUpdateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(TaskUpdateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.TASKS}/{request.dto.Id}";

        return await apiClient.PutAsync<TaskDetailsDto, bool>(baseUrl, request.dto, cancellationToken);
    }
}
