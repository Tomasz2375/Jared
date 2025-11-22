using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Tasks;

public sealed record TaskListQuery(int? projectId, int? epicId)
    : IRequest<Result<List<TaskListDto>>>;
