using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.Role;
using MediatR;

namespace Jared.Presentation.Requests.Roles.List;

public sealed record RoleListQuery : IRequest<Result<List<RoleListDto>>>;
