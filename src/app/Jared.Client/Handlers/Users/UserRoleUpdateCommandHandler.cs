using Jared.Client.Abstractions;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Client.Requests.Users;

public class UserRoleUpdateCommandHandler(IApiClient apiClient)
    : IRequestHandler<UserRoleUpdateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UserRoleUpdateCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{BaseAdresses.USERS}/{request.dto.Id}/role";

        return await apiClient.PatchAsync<UserRoleUpdateDto, bool>(baseUrl, request.dto, cancellationToken).ConfigureAwait(false);
    }
}
