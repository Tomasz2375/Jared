using Jared.Application.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;

namespace Jared.Presentation.Requests.Epics.Page;

public sealed record EpicPageQuery(Query query) : IRequest<Result<EpicPageDto>>;
