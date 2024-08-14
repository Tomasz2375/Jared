using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Users.Password;

public sealed record UserPasswordCommand(UserPasswordDto dto) : IRequest<Result<bool>>;
