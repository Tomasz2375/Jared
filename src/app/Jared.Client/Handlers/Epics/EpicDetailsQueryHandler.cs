using Jared.Client.Abstractions;
using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using Jared.Dtos.Epics;
using MediatR;

namespace Jared.Client.Handlers.Epics;

public class EpicDetailsQueryHandler(IApiClient apiClient)
    : IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>
{
    public async Task<Result<EpicDetailsDto>> Handle(EpicDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.EPICS}/{request.id}";

        return await apiClient.GetAsync<EpicDetailsDto>(baseUrl, cancellationToken);
    }
}
