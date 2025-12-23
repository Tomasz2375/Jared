using Jared.Client.Abstractions;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Client.Handlers.Users;

public class UserDetailsQueryHandler(IApiClient apiClient)
    : IRequestHandler<UserDetailsQuery, Result<UserDetailsDto>>
{
    public async Task<Result<UserDetailsDto>> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.USERS}/{request.id}";

        return await apiClient.GetAsync<UserDetailsDto>(baseUrl, cancellationToken);
    }
}
