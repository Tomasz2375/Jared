using Jared.Application.Dtos.TaskDto;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;

namespace Jared.Application.Queries.TaskQueries;

public sealed record class TaskListQuery(
    int page = 1,
    int pageSize = (int)PageSize.Ten,
    string? filter = null,
    string? sortingProperty = null,
    SortingDirection? SortingDirection = null)
    : IRequest<Result<TaskPageDto>>;

