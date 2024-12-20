﻿using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.Epics.Details;

public class EpicDetailsQueryHandler(HttpClient httpClient)
    : IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<EpicDetailsDto>> Handle(EpicDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.EPIC_DETAILS}/{request.id}";

        var response = await httpClient.GetFromJsonAsync<Result<EpicDetailsDto>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<EpicDetailsDto>("Invalid response type");
        }

        return response;
    }
}
