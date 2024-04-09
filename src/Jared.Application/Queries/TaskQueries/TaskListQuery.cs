using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Queries.TaskQueries;

public record TaskListQuery(int? projectId, int? epicId) : IRequest<Result<List<TaskListDto>>>;
