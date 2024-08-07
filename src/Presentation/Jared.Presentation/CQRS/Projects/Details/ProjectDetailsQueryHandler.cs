﻿using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Projects.Details;

public class ProjectDetailsQueryHandler : IRequestHandler<ProjectDetailsQuery, Result<ProjectDetailsDto>>
{
    private readonly HttpClient httpClient;

    public ProjectDetailsQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<ProjectDetailsDto>> Handle(ProjectDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.PROJECT_DETAILS}/{request.id}";

        return await httpClient.GetFromJsonAsync<Result<ProjectDetailsDto>>(baseUrl);
    }
}
