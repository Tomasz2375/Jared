﻿using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.User.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, Result<bool>>
{
    private readonly HttpClient httpClient;

    public UserRegisterCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<bool>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.USER_REGISTER;

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