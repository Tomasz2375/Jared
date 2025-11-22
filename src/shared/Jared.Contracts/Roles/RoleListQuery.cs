using Jared.Dtos.Roles;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Roles;

public sealed record RoleListQuery
    : IRequest<Result<List<RoleListDto>>>;
