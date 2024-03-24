using Jared.Application.Dtos.TaskDto;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.TaskCommand;

public sealed record CreateTaskCommand(TaskDetailsDto dto) : IRequest<Result>;
