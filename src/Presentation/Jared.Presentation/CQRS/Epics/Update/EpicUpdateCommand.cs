using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.Epics.Update;

public sealed record EpicUpdateCommand(EpicDetailsDto dto) : IRequest<Result<bool>>;
