using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.User.Password;

public sealed record UserPasswordCommand(UserPasswordDto dto) : IRequest<Result<bool>>;
