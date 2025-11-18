using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;

namespace Jared.Application.Requests.Users.Register;

public sealed record UserRegisterCommand(UserRegisterDto dto) : IRequest<Result<bool>>;
