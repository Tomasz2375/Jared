using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Epics.List;

public sealed record EpicListQuery(int? projectId) : IRequest<Result<List<EpicListDto>>>;
