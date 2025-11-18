using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using MediatR;

namespace Jared.Application.Requests.Tasks.List;

public sealed record TaskListQuery(int? projectId, int? epicId) : IRequest<Result<List<TaskListDto>>>;
