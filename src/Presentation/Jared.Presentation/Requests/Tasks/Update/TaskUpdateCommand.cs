using Jared.Application.Dtos.TaskDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Tasks.Update;

public sealed record TaskUpdateCommand(TaskDetailsDto dto) : IRequest<Result<bool>>;
