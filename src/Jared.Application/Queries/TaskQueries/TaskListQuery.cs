using Jared.Application.Dtos.TaskDto;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;

namespace Jared.Application.Queries.TaskQueries;

public sealed record class TaskListQuery(
    int? page = null,
    int? pageSize = null,
    string? filter = null,
    string? sortingProperty = null,
    SortingDirection? SortingDirection = null)
    : IRequest<Result<List<TaskListDto>>>;

