using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Users;

public sealed record UserUpdateCommand(UserUpdateDto dto)
    : IRequest<Result<bool>>;
