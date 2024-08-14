using Jared.Application.Dtos.TaskDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Tasks.Details;

public sealed record TaskDetailsQuery(int id) : IRequest<Result<TaskDetailsDto>>;
