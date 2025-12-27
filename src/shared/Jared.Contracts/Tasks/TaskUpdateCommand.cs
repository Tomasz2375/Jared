using Jared.Core.Abstractions;
using Jared.Dtos.Tasks;
using MediatR;

namespace Jared.Contracts.Tasks;

public sealed record TaskUpdateCommand(TaskDetailsDto dto)
    : IRequest<Result<TaskDetailsDto>>;
