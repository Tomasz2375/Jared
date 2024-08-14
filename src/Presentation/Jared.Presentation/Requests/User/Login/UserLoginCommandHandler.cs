using Jared.Shared.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.User.Login;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<string>>
{
    private readonly HttpClient httpClient;

    public UserLoginCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.USER_LOGIN;

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
