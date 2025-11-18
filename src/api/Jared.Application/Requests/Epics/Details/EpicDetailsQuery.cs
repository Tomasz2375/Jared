using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;

namespace Jared.Application.Requests.Epics.Details;

public sealed record EpicDetailsQuery(int id) : IRequest<Result<EpicDetailsDto>>;
