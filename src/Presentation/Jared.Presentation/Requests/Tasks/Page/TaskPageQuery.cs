using Jared.Presentation.ColumnDefinitions;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using MediatR;

namespace Jared.Presentation.Requests.Tasks.Page;

public sealed record TaskPageQuery(Query query) : IRequest<Result<TaskPageDto>>;
