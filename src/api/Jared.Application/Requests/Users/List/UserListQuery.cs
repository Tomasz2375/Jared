using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;

namespace Jared.Application.Requests.Users.List;

public sealed record UserListQuery : IRequest<Result<List<UserListDto>>>;
