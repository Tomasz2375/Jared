using Jared.Application.Dtos.TaskDtos;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Application.Requests.Tasks.Page;

public sealed record TaskPageQuery(
    int page,
    int pageSize,
    string? sortingProperty,
    SortingDirection? SortingDirection,
    IDictionary<string, string?>? filters)
    : IRequest<Result<TaskPageDto>>;

