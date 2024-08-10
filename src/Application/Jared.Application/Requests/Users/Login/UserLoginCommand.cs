using Jared.Application.Dtos.UserDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Users.Login;

public sealed record UserLoginCommand(UserLoginDto dto) : IRequest<Result<string>>;
