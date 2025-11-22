using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Tasks;

public sealed record TaskDetailsQuery(int id)
    : IRequest<Result<TaskDetailsDto>>;
