using Jared.Client.Abstractions;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Dtos.Tasks;
using MediatR;

namespace Jared.Client.Handlers.Tasks;

public class TaskCreateCommandHandler(IApiClient apiClient)
    : IRequestHandler<TaskCreateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(TaskCreateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.TASKS;

        return await apiClient.PostAsync<TaskDetailsDto, bool>(baseUrl, request.dto, cancellationToken);
    }
}
