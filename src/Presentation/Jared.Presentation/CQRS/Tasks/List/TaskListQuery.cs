﻿using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.Tasks.List;

public sealed record TaskListQuery(int? projectId, int? epicId): IRequest<Result<List<TaskListDto>>>;
