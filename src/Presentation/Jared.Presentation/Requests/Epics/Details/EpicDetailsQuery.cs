using Jared.Application.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Epics.Details;

public sealed record EpicDetailsQuery(int id) : IRequest<Result<EpicDetailsDto>>;
