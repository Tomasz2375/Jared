using System.Net.Http.Json;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Client.Requests.Users;

public class UserListQueryHandler(HttpClient httpClient)
    : IRequestHandler<UserListQuery, Result<List<UserListDto>>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<List<UserListDto>>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.USERS;

        var response = await httpClient.GetFromJsonAsync<Result<List<UserListDto>>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<List<UserListDto>>("Invalid response type");
        }

        return response;
    }
}
