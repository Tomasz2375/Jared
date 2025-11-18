using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Tasks.List;

public class TaskListQueryHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<TaskListQuery, Result<List<TaskListDto>>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<List<TaskListDto>>> Handle(TaskListQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var taskQuery = dataContext
                .Set<Domain.Models.Task>()
                .Where(x => query.projectId == null || query.projectId == 0 || x.ProjectId == query.projectId)
                .Where(x => query.epicId == null || query.epicId == 0 || x.EpicId == query.epicId)
                .AsNoTracking();

            var tasks = await taskQuery.ToListAsync();

            var result = mapper.Map<List<TaskListDto>>(tasks);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<TaskListDto>>(ex.Message);
        }
    }
}
