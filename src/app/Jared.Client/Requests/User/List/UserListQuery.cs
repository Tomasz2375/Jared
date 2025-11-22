using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.User.List;

public sealed record UserListQuery : IRequest<Result<List<UserListDto>>>;
