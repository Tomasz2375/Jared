using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Epics.Create;

public sealed record EpicCreateCommand(EpicDetailsDto dto)
    : IRequest<Result<bool>>;
