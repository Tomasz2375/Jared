using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;

namespace Jared.Presentation.Requests.Epics.List;

public sealed record EpicListQuery(int? projectId) : IRequest<Result<List<EpicListDto>>>;
