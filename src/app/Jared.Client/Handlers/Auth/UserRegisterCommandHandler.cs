using System.Net.Http.Json;
using Jared.Client.Handlers;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using MediatR;

namespace Jared.Client.Handlers.Auth;

public class UserRegisterCommandHandler(HttpClient httpClient)
    : IRequestHandler<UserRegisterCommand, Result<bool>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<bool>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.REGISTER;

        var result = await httpClient.PostAsJsonAsync(baseUrl, request.dto, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            return Result.Fail<bool>($"Something went wrong. Status code: {(int)result.StatusCode} ({result.StatusCode})");
        }

        var response = await result.Content.ReadFromJsonAsync<Result<bool>>();

        if (response is null)
        {
            return Result.Fail<bool>("Invalid response type");
        }

        return response;
    }
}
