using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Epics.Update;

public sealed record EpicUpdateCommand(EpicDetailsDto dto) : IRequest<Result<bool>>;
