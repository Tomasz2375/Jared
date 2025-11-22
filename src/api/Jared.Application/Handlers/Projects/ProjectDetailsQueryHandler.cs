using Jared.Contracts.Projects;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Projects;

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
