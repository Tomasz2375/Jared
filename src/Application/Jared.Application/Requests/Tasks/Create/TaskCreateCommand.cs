﻿using Jared.Application.Dtos.TaskDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Tasks.Create;

public sealed record TaskCreateCommand(TaskDetailsDto dto) : IRequest<Result<bool>>;
