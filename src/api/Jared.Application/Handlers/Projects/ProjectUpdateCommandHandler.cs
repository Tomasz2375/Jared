using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Projects;

public class ProjectUpdateCommandHandler(IDataContext dataContext)
    : IRequestHandler<ProjectUpdateCommand, Result<bool>>
{
    private readonly IDataContext dataContext = dataContext;

    public async Task<Result<bool>> Handle(ProjectUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var project = await dataContext.Set<Project>()
                .FirstAsync(x => x.Id == command.dto.Id);

            command.dto.Adapt(project);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
