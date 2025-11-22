using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Epics;

public sealed record EpicUpdateCommand(EpicDetailsDto dto)
    : IRequest<Result<bool>>;
