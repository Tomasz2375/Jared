using Jared.Application.Dtos.TaskDto;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using MediatR;

namespace Jared.Application.Commands.Task;

public sealed record CreateTaskCommand(TaskDetailsDto dto) : IRequest<Result>;
