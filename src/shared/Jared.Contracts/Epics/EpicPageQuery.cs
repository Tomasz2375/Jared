using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Dtos.Epics;
using MediatR;

namespace Jared.Contracts.Epics;

public sealed record EpicPageQuery(
    int page = 1,
    int pageSize = 100,
    string? sortingProperty = null,
    SortingDirection? sortingDirection = null,
    IDictionary<string, string?>? filters = null)
    : IRequest<Result<EpicPageDto>>;
