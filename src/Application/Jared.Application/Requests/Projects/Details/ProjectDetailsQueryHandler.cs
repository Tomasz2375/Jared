﻿using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Projects.Details;

public class ProjectDetailsQueryHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<ProjectDetailsQuery, Result<ProjectDetailsDto>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<ProjectDetailsDto>> Handle(ProjectDetailsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var project = await dataContext.Set<Project>()
                .AsNoTracking()
                .FirstAsync(x => x.Id == query.id, cancellationToken);

            var result = mapper.Map<ProjectDetailsDto>(project);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<ProjectDetailsDto>(ex.Message);
        }
    }
}
