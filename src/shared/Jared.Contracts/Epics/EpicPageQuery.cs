using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Contracts.Epics;

public sealed record EpicPageQuery(
    int page = 1,
    int pageSize = 100,
    string? sortingProperty = null,
    SortingDirection? sortingDirection = null,
    IDictionary<string, string?>? filters = null)
    : IRequest<Result<EpicPageDto>>;
