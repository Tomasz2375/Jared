using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.User.Login;

public sealed record UserLoginCommand(UserLoginDto dto) : IRequest<Result<string>>;
