﻿using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Projects.List;

public class ProjectListQueryHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<ProjectListQuery, Result<List<ProjectListDto>>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<List<ProjectListDto>>> Handle(ProjectListQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var projectsQuery = dataContext
                .Set<Project>()
                .AsNoTracking();

            var projects = await projectsQuery.ToListAsync();

            var result = mapper.Map<List<ProjectListDto>>(projects);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ProjectListDto>>(ex.Message);
        }
    }
}
