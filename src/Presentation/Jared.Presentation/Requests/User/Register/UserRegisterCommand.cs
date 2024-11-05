using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;

namespace Jared.Presentation.Requests.User.Register;

public sealed record UserRegisterCommand(UserRegisterDto dto) : IRequest<Result<bool>>;
