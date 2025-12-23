using Jared.Client.Abstractions;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Client.Requests.Users;

public class UserPasswordCommandHandler(IApiClient apiClient)
    : IRequestHandler<UserPasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UserPasswordCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{BaseAdresses.USERS}/{request.dto.Id}/password";

        return await apiClient.PatchAsync<UserPasswordDto, bool>(baseUrl, request.dto, cancellationToken);
    }
}
