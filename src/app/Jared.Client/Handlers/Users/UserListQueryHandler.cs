using Jared.Client.Abstractions;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Client.Requests.Users;

public class UserListQueryHandler(IApiClient apiClient)
    : IRequestHandler<UserListQuery, Result<List<UserListDto>>>
{
    public async Task<Result<List<UserListDto>>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.USERS;

        return await apiClient.GetAsync<List<UserListDto>>(baseUrl, cancellationToken);
    }
}
