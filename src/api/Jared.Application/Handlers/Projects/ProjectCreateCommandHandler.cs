using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;

namespace Jared.Application.Handlers.Projects;

public class ProjectCreateCommandHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<ProjectCreateCommand, Result<bool>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<bool>> Handle(ProjectCreateCommand command, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Project>(command.dto);

        try
        {
            dataContext.Add(project);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
