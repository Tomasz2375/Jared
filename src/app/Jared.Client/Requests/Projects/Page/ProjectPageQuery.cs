using Jared.Client.ColumnDefinitions;
using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Projects.Page;

public sealed record ProjectPageQuery(Query query) : IRequest<Result<ProjectPageDto>>;
