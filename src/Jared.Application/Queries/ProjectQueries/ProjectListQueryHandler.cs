using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Queries.ProjectQueries;

public class ProjectListQueryHandler : IRequestHandler<ProjectListQuery, Result<List<ProjectListDto>>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public ProjectListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

#pragma warning disable CS1998
    public async Task<Result<List<ProjectListDto>>> Handle(ProjectListQuery request, CancellationToken cancellationToken)
#pragma warning disable CS1998
    {
        var projectsQuery = dataContext
            .Set<Project>()
            .AsNoTracking();

        var projects = projectsQuery.AsEnumerable();

        var result = mapper.Map<List<ProjectListDto>>(projects);

        return Result.Ok(result);
    }
}
