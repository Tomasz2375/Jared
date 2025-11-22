using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.User.Update;

public sealed record UserUpdateCommand(UserUpdateDto dto) : IRequest<Result<bool>>;
