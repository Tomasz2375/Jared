﻿using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.Epics.List;

public sealed record EpicListQuery(int? projectId) : IRequest<Result<List<EpicListDto>>>;
