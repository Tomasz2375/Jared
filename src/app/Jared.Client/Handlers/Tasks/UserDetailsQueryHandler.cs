using System.Net.Http.Json;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Client.Handlers.Tasks;

public class UserDetailsQueryHandler(HttpClient httpClient)
    : IRequestHandler<UserDetailsQuery, Result<UserDetailsDto>>
{
    public async Task<Result<UserDetailsDto>> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.USERS}/{request.id}";

        var response = await httpClient.GetFromJsonAsync<Result<UserDetailsDto>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<UserDetailsDto>("Invalid response type");
        }

        return response;
    }
}
