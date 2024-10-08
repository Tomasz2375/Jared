﻿using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using Jared.Shared.Enums;
using MediatR;

namespace Jared.Application.Requests.Epics.Page;

public sealed record EpicPageQuery(
    int page,
    int pageSize,
    string? sortingProperty,
    SortingDirection? SortingDirection,
    IDictionary<string, string?>? filter)
    : IRequest<Result<EpicPageDto>>;
