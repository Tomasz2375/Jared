using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Application.Requests.Tasks.Page;

public sealed record TaskPageQuery(
    int page,
    int pageSize,
    string? sortingProperty,
    SortingDirection? sortingDirection,
    IDictionary<string, string?>? filters)
    : IRequest<Result<TaskPageDto>>;
