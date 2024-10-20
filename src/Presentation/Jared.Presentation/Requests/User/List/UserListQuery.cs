using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;

namespace Jared.Presentation.Requests.User.List;

public sealed record UserListQuery : IRequest<Result<List<UserListDto>>>;
