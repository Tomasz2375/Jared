using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;

namespace Jared.Presentation.Requests.Tasks.Page;

public sealed record TaskPageQuery(Query query) : IRequest<Result<TaskPageDto>>;
