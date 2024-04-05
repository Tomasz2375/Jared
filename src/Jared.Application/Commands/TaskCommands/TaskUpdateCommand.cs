using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.TaskCommands;

public record TaskUpdateCommand(TaskDetailsDto dto) : IRequest<Result<TaskDetailsDto>>;
