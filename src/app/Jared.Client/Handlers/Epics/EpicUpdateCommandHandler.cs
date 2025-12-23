using Jared.Client.Abstractions;
using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using Jared.Dtos.Epics;
using MediatR;

namespace Jared.Client.Handlers.Epics;

public class EpicUpdateCommandHandler(IApiClient apiClient)
    : IRequestHandler<EpicUpdateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(EpicUpdateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.EPICS}/{request.dto.Id}";

        return await apiClient.PutAsync<EpicDetailsDto, bool>(baseUrl, request.dto, cancellationToken);
    }
}
