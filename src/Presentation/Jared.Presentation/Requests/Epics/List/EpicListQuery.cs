using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Epics.List;

public sealed record EpicListQuery(int? projectId) : IRequest<Result<List<EpicListDto>>>;
