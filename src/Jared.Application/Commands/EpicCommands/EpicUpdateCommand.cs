using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.EpicCommands;

public sealed record EpicUpdateCommand(EpicDetailsDto dto) : IRequest<Result>;
