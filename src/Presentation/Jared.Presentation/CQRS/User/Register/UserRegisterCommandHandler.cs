using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.User.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, Result>
{
    private readonly HttpClient httpClient;

    public UserRegisterCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.USER_REGISTER;

        var result = await httpClient.PostAsJsonAsync(baseUrl, request.dto, cancellationToken);

        return Result.Ok();
    }
}
