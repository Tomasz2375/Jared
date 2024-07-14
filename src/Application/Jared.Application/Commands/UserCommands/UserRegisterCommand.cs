using Jared.Application.Dtos.UserDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.UserCommands;

public sealed record UserRegisterCommand(UserRegisterDto dto) : IRequest<Result>;
