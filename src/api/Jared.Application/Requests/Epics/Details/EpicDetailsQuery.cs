using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Epics.Details;

public sealed record EpicDetailsQuery(int id) : IRequest<Result<EpicDetailsDto>>;
