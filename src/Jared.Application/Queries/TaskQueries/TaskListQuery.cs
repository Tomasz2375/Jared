using Jared.Application.Dtos.TaskDto;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Queries.TaskQueries;

public sealed record class TaskListQuery : IRequest<Result<List<TaskListDto>>>;

