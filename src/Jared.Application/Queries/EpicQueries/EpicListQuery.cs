using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Queries.EpicQueries;

public record EpicListQuery(int? projectId) : IRequest<Result<List<EpicListDto>>>;
