using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Contracts.Epics;

public sealed record EpicPageQuery(
    int page,
    int pageSize,
    string? sortingProperty,
    SortingDirection? sortingDirection,
    IDictionary<string, string?>? filter)
    : IRequest<Result<EpicPageDto>>;
