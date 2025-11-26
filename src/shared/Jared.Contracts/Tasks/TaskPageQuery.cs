using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Contracts.Tasks;

public sealed record TaskPageQuery(
    int page = 1,
    int pageSize = 100,
    string? sortingProperty = null,
    SortingDirection? sortingDirection = null,
    IDictionary<string, string?>? filters = null)
    : IRequest<Result<TaskPageDto>>;
