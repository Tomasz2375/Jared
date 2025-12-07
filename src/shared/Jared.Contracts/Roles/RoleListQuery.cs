using Jared.Core.Abstractions;
using Jared.Dtos.Roles;
using MediatR;

namespace Jared.Contracts.Roles;

public sealed record RoleListQuery
    : IRequest<Result<List<RoleListDto>>>;
