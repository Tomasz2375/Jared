using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Contracts.Projects;

public sealed record ProjectPageQuery(
    int page,
    int pageSize,
    string? sortingProperty,
    SortingDirection? sortingDirection,
    IDictionary<string, string?>? filters)
    : IRequest<Result<ProjectPageDto>>;
