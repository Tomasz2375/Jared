using Jared.Application.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Epics.Update;

public sealed record EpicUpdateCommand(EpicDetailsDto dto) : IRequest<Result<bool>>;
