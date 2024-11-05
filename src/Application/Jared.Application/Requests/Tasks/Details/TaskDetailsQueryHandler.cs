using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Tasks.Details;

public class TaskDetailsQueryHandler : IRequestHandler<TaskDetailsQuery, Result<TaskDetailsDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public TaskDetailsQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<TaskDetailsDto>> Handle(TaskDetailsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var task = await dataContext.Set<Domain.Models.Task>()
                .AsNoTracking()
                .Include(x => x.Project)
                .Include(x => x.Epic)
                .Include(x => x.TaskHistories)
                .FirstAsync(x => x.Id == query.id, cancellationToken);

            var result = mapper.Map<TaskDetailsDto>(task);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<TaskDetailsDto>(ex.Message);
        }
    }
}
