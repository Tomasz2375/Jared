using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Epics.Create;

public sealed record EpicCreateCommand(EpicDetailsDto dto) : IRequest<Result<bool>>;
