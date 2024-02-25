using Jared.Application.Dtos.TaskDto;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Queries.TaskQueries;

public class TaskListQueryHandler : IRequestHandler<TaskListQuery, Result<List<TaskListDto>>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public TaskListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<List<TaskListDto>>> Handle(TaskListQuery request, CancellationToken cancellationToken)
    {
        var tasks = await dataContext.Set<Domain.Models.Task>().ToListAsync(cancellationToken);
        
        var result = mapper.Map<List<TaskListDto>>(tasks);

        return Result.Ok(result);
    }
}
