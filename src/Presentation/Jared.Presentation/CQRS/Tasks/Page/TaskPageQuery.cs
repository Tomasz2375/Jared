using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;

namespace Jared.Presentation.CQRS.Tasks.Page;

public sealed record TaskPageQuery(Query query) : IRequest<Result<TaskPageDto>>;
