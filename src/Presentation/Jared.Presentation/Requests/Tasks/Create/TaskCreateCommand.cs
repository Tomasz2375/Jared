using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using MediatR;

namespace Jared.Presentation.Requests.Tasks.Create;

public sealed record TaskCreateCommand(TaskDetailsDto dto) : IRequest<Result<bool>>;
