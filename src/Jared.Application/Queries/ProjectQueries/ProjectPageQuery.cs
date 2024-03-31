using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;

namespace Jared.Application.Queries.ProjectQueries;

public sealed record ProjectPageQuery(
    int page,
    int pageSize,
    string? filter,
    string? sortingProperty,
    SortingDirection? SortingDirection)
    : IRequest<Result<ProjectPageDto>>;
