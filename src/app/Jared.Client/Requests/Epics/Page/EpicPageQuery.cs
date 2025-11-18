using Jared.Client.ColumnDefinitions;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;

namespace Jared.Client.Requests.Epics.Page;

public sealed record EpicPageQuery(Query query) : IRequest<Result<EpicPageDto>>;
