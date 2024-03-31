using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using MediatR;

namespace Jared.Application.Queries.EpicQueries;

public record EpicPageQuery(
    int page,
    int pageSize,
    string? filter,
    string? sortingProperty,
    SortingDirection? SortingDirection)
    : IRequest<Result<EpicPageDto>>;
