using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;

namespace Jared.Presentation.Requests.Epics.Update;

public sealed record EpicUpdateCommand(EpicDetailsDto dto) : IRequest<Result<bool>>;
