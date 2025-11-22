using Jared.Client.ColumnDefinitions;
using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Tasks.Page;

public sealed record TaskPageQuery(Query query) : IRequest<Result<TaskPageDto>>;
