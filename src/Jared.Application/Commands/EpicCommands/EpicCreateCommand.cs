using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.EpicCommands;

public record EpicCreateCommand(EpicDetailsDto dto) : IRequest<Result>;
