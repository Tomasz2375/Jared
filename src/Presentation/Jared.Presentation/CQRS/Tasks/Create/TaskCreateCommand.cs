using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.Tasks.Create;

public sealed record TaskCreateCommand(TaskDetailsDto dto) : IRequest<Result>;
