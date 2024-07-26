using Jared.Application.Dtos.UserDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.User.Login;

public sealed record UserLoginCommand(UserLoginDto dto) : IRequest<Result<string>>;
