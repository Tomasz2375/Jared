using Jared.Application.Dtos.UserDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Users.Password;

public sealed record UserPasswordCommand(UserPasswordDto dto) : IRequest<Result<bool>>;
