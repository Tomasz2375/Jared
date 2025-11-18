using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;

namespace Jared.Client.Requests.User.Update;

public sealed record UserUpdateCommand(UserUpdateDto dto) : IRequest<Result<bool>>;
