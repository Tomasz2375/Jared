using Jared.Shared.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.User.Update;

public class UserUpdateCommandHandler(HttpClient httpClient)
    : IRequestHandler<UserUpdateCommand, Result<bool>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<bool>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.USER_UPDATE;

        var result = await httpClient.PutAsJsonAsync(baseUrl, request.dto, cancellationToken).ConfigureAwait(false);

        if (!result.IsSuccessStatusCode)
        {
            return Result.Fail<bool>($"User update failed. Status code: {(int)result.StatusCode} ({result.StatusCode})");
        }

        var response = await result.Content.ReadFromJsonAsync<Result<bool>>();

        if (response is null)
        {
            return Result.Fail<bool>("Invalid response value");
        }

        return response;
    }
}
