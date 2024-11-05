using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;

namespace Jared.Application.Requests.Epics.List;

public sealed record EpicListQuery(int? projectId) : IRequest<Result<List<EpicListDto>>>;
