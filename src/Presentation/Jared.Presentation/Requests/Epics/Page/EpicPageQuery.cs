using Jared.Presentation.ColumnDefinitions;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;

namespace Jared.Presentation.Requests.Epics.Page;

public sealed record EpicPageQuery(Query query) : IRequest<Result<EpicPageDto>>;
