﻿using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Application.Requests.Projects.Page;

public sealed record ProjectPageQuery(
    int page,
    int pageSize,
    string? sortingProperty,
    SortingDirection? SortingDirection,
    IDictionary<string, string?>? filters)
    : IRequest<Result<ProjectPageDto>>;
