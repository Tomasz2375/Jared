using System.Net.Http.Json;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Users;

public class UserRoleUpdateCommandHandler(HttpClient httpClient)
    : IRequestHandler<UserRoleUpdateCommand, Result<bool>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<bool>> Handle(UserRoleUpdateCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{BaseAdresses.USERS}/{request.dto.Id}/role";

        var result = await httpClient.PatchAsJsonAsync(baseUrl, request.dto, cancellationToken).ConfigureAwait(false);

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
