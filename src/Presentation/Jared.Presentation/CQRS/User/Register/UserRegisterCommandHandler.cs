using Jared.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        var baseUrl = "User/Register";

        var result = await httpClient.PostAsJsonAsync(baseUrl, request.dto, cancellationToken);

        // var test = await result.Content.ReadFromJsonAsync<ActionResult<Result>>();

        return Result.Ok();
    }
}
