using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;

namespace Jared.Client.Requests.User.Login;

public sealed record UserLoginCommand(UserLoginDto dto) : IRequest<Result<string>>;
