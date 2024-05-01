using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Queries.EpicQueries;

public sealed record EpicDetailsQuery(int id) : IRequest<Result<EpicDetailsDto>>;
