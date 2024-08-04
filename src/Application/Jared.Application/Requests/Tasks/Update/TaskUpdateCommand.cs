using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Tasks.Update;

public record TaskUpdateCommand(TaskDetailsDto dto) : IRequest<Result>;
