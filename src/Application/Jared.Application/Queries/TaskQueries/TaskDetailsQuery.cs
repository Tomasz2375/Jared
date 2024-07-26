using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Queries.TaskQueries;

public record TaskDetailsQuery(int id) : IRequest<Result<TaskDetailsDto>>;
