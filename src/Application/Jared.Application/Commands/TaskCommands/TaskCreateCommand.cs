using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.TaskCommand;

public sealed record TaskCreateCommand(TaskDetailsDto dto) : IRequest<Result>;
