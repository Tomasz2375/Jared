using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Contracts.Tasks;

public sealed record TaskPageQuery(
    int page,
    int pageSize,
    string? sortingProperty,
    SortingDirection? sortingDirection,
    IDictionary<string, string?>? filters)
    : IRequest<Result<TaskPageDto>>;
