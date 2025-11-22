using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Tasks.List;

public sealed record TaskListQuery(int? projectId, int? epicId)
    : IRequest<Result<List<TaskListDto>>>;
