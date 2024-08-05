using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Epics.Create;

public record EpicCreateCommand(EpicDetailsDto dto) : IRequest<Result<bool>>;
