using Jared.Shared.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.User.Password;

public class UserPasswordCommandHandler : IRequestHandler<UserPasswordCommand, Result<bool>>
{
    private readonly HttpClient httpClient;

    public UserPasswordCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<bool>> Handle(UserPasswordCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.USER_PASSWORD;

        var result = await httpClient.PutAsJsonAsync(baseUrl, request.dto, cancellationToken);

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
