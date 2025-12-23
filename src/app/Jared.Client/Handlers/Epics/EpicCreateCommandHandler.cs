using Jared.Client.Abstractions;
using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using Jared.Dtos.Epics;
using MediatR;

namespace Jared.Client.Handlers.Epics;

public class EpicCreateCommandHandler(IApiClient apiClient)
    : IRequestHandler<EpicCreateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(EpicCreateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.EPICS;

        return await apiClient.PostAsync<EpicDetailsDto, bool>(baseUrl, request.dto, cancellationToken);
    }
}
