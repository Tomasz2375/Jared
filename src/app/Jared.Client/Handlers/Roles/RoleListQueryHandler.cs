using Jared.Client.Abstractions;
using Jared.Contracts.Roles;
using Jared.Core.Abstractions;
using Jared.Dtos.Roles;
using MediatR;

namespace Jared.Client.Handlers.Roles;

public class RoleListQueryHandler(IApiClient apiClient)
    : IRequestHandler<RoleListQuery, Result<List<RoleListDto>>>
{
    public async Task<Result<List<RoleListDto>>> Handle(RoleListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.ROLES;

        return await apiClient.GetAsync<List<RoleListDto>>(baseUrl, cancellationToken);
    }
}
