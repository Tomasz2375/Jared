using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.Tasks.Update;

public sealed record TaskUpdateCommand(TaskDetailsDto dto) : IRequest<Result>;
