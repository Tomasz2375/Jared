using Jared.Application.Dtos.UserDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.User.Register;

public sealed record UserRegisterCommand(UserRegisterDto dto) : IRequest<Result<bool>>;
