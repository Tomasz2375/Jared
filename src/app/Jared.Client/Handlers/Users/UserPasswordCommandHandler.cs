using System.Net.Http.Json;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Users;

public class UserPasswordCommandHandler(HttpClient httpClient)
    : IRequestHandler<UserPasswordCommand, Result<bool>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<bool>> Handle(UserPasswordCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{BaseAdresses.USERS}/{request.dto.Id}/password";

        var result = await httpClient.PatchAsJsonAsync(baseUrl, request.dto, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            return Result.Fail<bool>($"Password change failed. Status code: {(int)result.StatusCode} ({result.StatusCode})");
        }

        var response = await result.Content.ReadFromJsonAsync<Result<bool>>();

        if (response is null)
        {
            return Result.Fail<bool>("Invalid response value");
        }

        return response;
    }
}
