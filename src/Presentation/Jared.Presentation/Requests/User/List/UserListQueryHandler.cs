using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.User.List;

public class UserListQueryHandler : IRequestHandler<UserListQuery, Result<List<UserListDto>>>
{
    private readonly HttpClient httpClient;

    public UserListQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<List<UserListDto>>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.USER_LIST;

        var response = await httpClient.GetFromJsonAsync<Result<List<UserListDto>>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<List<UserListDto>>("Invalid response type");
        }

        return response;
    }
}
