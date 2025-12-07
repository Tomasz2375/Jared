using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Contracts.Users;

public sealed record UserRoleUpdateCommand(UserRoleUpdateDto dto)
    : IRequest<Result<bool>>;
