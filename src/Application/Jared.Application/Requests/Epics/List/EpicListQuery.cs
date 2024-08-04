using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Epics.List;

public record EpicListQuery(int? projectId) : IRequest<Result<List<EpicListDto>>>;
