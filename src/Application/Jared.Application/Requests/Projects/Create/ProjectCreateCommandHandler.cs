using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;

namespace Jared.Application.Requests.Projects.Create;

public class ProjectCreateCommandHandler : IRequestHandler<ProjectCreateCommand, Result<bool>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public ProjectCreateCommandHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

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
