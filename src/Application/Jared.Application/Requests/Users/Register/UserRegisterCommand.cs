using Jared.Application.Dtos.UserDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Users.Register;

public sealed record UserRegisterCommand(UserRegisterDto dto) : IRequest<Result<bool>>;
