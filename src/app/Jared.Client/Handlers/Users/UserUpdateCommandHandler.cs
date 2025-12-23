using Jared.Client.Abstractions;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Client.Requests.Users;

public class UserUpdateCommandHandler(IApiClient apiClient)
    : IRequestHandler<UserUpdateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{BaseAdresses.USERS}/{request.dto.Id}";

        return await apiClient.PutAsync<UserDetailsDto, bool>(baseUrl, request.dto, cancellationToken).ConfigureAwait(false);
    }
}
