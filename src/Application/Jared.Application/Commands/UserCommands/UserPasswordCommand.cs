using Jared.Application.Dtos.UserDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.UserCommands;

public sealed record UserPasswordCommand(UserPasswordDto dto) : IRequest<Result<bool>>;
