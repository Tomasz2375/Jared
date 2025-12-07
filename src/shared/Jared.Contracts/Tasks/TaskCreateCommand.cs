using Jared.Core.Abstractions;
using Jared.Dtos.Tasks;
using MediatR;

namespace Jared.Contracts.Tasks;

public sealed record TaskCreateCommand(TaskDetailsDto dto)
    : IRequest<Result<bool>>;
