using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;

namespace Jared.Application.Requests.Projects.Create;

public class ProjectCreateCommandHandler : IRequestHandler<ProjectCreateCommand, Result>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public ProjectCreateCommandHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

#pragma warning disable CS1998
    public async Task<Result> Handle(ProjectCreateCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998
    {
        var project = mapper.Map<Project>(command.dto);

        try
        {
            dataContext.Add(project);
            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
