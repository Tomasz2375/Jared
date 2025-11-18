using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using MediatR;

namespace Jared.Application.Requests.Tasks.Update;

public sealed record TaskUpdateCommand(TaskDetailsDto dto) : IRequest<Result<bool>>;
