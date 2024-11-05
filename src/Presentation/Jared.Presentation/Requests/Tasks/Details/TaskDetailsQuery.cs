using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using MediatR;

namespace Jared.Presentation.Requests.Tasks.Details;

public sealed record TaskDetailsQuery(int id) : IRequest<Result<TaskDetailsDto>>;
