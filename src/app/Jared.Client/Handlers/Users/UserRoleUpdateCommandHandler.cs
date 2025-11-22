using System.Net.Http.Json;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Users;

public class UserRoleUpdateCommandHandler(HttpClient httpClient)
    : IRequestHandler<UserRoleUpdateCommand, Result<bool>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<bool>> Handle(UserRoleUpdateCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.USER_ROLE_UPDATE;

        var result = await httpClient.PutAsJsonAsync(baseUrl, request.dto, cancellationToken).ConfigureAwait(false);

        if (!result.IsSuccessStatusCode)
        {
            return Result.Fail<bool>($"User role update failed. Status code: {(int)result.StatusCode} ({result.StatusCode})");
        }

        var response = await result.Content.ReadFromJsonAsync<Result<bool>>();

        if (response is null)
        {
            return Result.Fail<bool>("Invalid response value");
        }

        return response;
    }
}
