using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using MediatR;

namespace Jared.Application.Requests.Epics.Create;

public sealed record EpicCreateCommand(EpicDetailsDto dto) : IRequest<Result<bool>>;
