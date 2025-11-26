using Jared.Core.Abstractions;
using Jared.Dtos.Epics;
using MediatR;

namespace Jared.Contracts.Epics;

public sealed record EpicDetailsQuery(int id)
    : IRequest<Result<EpicDetailsDto>>;
