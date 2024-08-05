using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.Epics.Create;

public sealed record EpicCreateCommand(EpicDetailsDto dto) : IRequest<Result<bool>>;
