using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Users.List;

public sealed record UserListQuery : IRequest<Result<List<UserListDto>>>;
