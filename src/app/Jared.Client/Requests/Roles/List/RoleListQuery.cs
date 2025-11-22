using Jared.Dtos.Roles;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Roles.List;

public sealed record RoleListQuery
    : IRequest<Result<List<RoleListDto>>>;
