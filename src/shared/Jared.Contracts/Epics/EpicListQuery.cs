using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Epics;

public sealed record EpicListQuery(int? projectId)
    : IRequest<Result<List<EpicListDto>>>;
