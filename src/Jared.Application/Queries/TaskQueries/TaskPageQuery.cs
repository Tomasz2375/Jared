using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;

namespace Jared.Application.Queries.TaskQueries;

public sealed record TaskPageQuery(
    int page,
    int pageSize,
    string? filter,
    string? sortingProperty,
    SortingDirection? SortingDirection)
    : IRequest<Result<TaskPageDto>>;

