using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Projects.List;

public class ProjectListQueryHandler : IRequestHandler<ProjectListQuery, Result<List<ProjectListDto>>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public ProjectListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<List<ProjectListDto>>> Handle(ProjectListQuery query, CancellationToken cancellationToken)
    {
        var projectsQuery = dataContext
            .Set<Project>()
            .AsNoTracking();

        var projects = await projectsQuery.ToListAsync();

        var result = mapper.Map<List<ProjectListDto>>(projects);

        return Result.Ok(result);
    }
}
