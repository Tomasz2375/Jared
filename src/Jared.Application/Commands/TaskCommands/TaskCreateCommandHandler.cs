﻿using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Commands.TaskCommand;

public class TaskCreateCommandHandler : IRequestHandler<TaskCreateCommand, Result>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public TaskCreateCommandHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

#pragma warning disable CS1998
    public async Task<Result> Handle(TaskCreateCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998
    {
        try
        {
            var project = await dataContext
                .Set<Project>()
                .FirstAsync(x => x.Id == command.dto.ProjectId, cancellationToken);

            project.LastTaskNumber++;
            command.dto.Code = createCode(project);
            var task = mapper.Map<Domain.Models.Task>(command.dto);

            dataContext.Add(task);
            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }

    }

    private string createCode(Project project)
    {
        return project.Code + " - " + project.LastTaskNumber;
    }
}
