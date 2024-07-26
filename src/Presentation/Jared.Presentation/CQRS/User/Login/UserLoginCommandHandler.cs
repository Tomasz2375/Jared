using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.User.Login;

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
            return Result.Fail<string>("Nieudana próba logowania");
        }

        // return await result.Content.ReadFromJsonAsync<Result<string>>();
        return await result.Content.ReadFromJsonAsync<Result<string>>();
    }
}
