using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Users.Update;

public sealed record UserUpdateCommand(UserUpdateDto dto) : IRequest<Result<bool>>;
