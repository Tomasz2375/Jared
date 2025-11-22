using Jared.Client.ColumnDefinitions;
using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Epics.Page;

public sealed record EpicPageQuery(Query query)
    : IRequest<Result<EpicPageDto>>;
