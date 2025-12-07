using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Dtos.Projects;
using MediatR;

namespace Jared.Contracts.Projects;

public sealed record ProjectPageQuery(
    int page = 1,
    int pageSize = 100,
    string? sortingProperty = null,
    SortingDirection? sortingDirection = null,
    IDictionary<string, string?>? filters = null)
    : IRequest<Result<ProjectPageDto>>;
