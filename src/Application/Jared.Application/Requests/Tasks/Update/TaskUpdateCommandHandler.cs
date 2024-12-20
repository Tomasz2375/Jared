﻿using Jared.Application.Services.TaskHistory;
using Jared.Application.Services.User;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Tasks.Update;

public class TaskUpdateCommandHandler(
    IDataContext dataContext,
    ITaskHistoryService taskHistoryService,
    IMapper mapper,
    IUserService userService)
    : IRequestHandler<TaskUpdateCommand, Result<bool>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly ITaskHistoryService taskHistoryService = taskHistoryService;
    private readonly IMapper mapper = mapper;
    private readonly IUserService userService = userService;

    public async Task<Result<bool>> Handle(TaskUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var userId = userService.GetUser().Id;
            var task = await dataContext.Set<Domain.Models.Task>()
                .Include(x => x.Project)
                .Include(x => x.Epic)
                .Include(x => x.TaskHistories)
                    .ThenInclude(x => x.User)
                .FirstAsync(x => x.Id == command.dto.Id);

            var changes = taskHistoryService.GetChanged(mapper.Map<TaskDetailsDto>(task), command.dto, userId);
            command.dto.TaskHistories.AddRange(changes);

            command.dto.Adapt(task);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
