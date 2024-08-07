﻿using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;

namespace Jared.Presentation.CQRS.Epics.Page;

public sealed record EpicPageQuery(Query query) : IRequest<Result<EpicPageDto>>;
