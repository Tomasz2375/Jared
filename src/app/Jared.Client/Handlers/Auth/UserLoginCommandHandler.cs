using System.Net.Http.Json;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using MediatR;

namespace Jared.Client.Handlers.Auth;

public class UserLoginCommandHandler(HttpClient httpClient)
    : IRequestHandler<UserLoginCommand, Result<string>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.LOGIN;

        var result = await httpClient.PostAsJsonAsync(baseUrl, request.dto, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            return Result.Fail<string>($"Login attempt failed. Status code: {(int)result.StatusCode} ({result.StatusCode})");
        }

        var response = await result.Content.ReadFromJsonAsync<Result<string>>();

        if (response is null)
        {
            return Result.Fail<string>("Invalid response type");
        }

        return response;
    }
}
