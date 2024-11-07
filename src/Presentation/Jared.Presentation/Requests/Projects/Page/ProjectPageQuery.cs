using Jared.Presentation.ColumnDefinitions;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using MediatR;

namespace Jared.Presentation.Requests.Projects.Page;

public sealed record ProjectPageQuery(Query query) : IRequest<Result<ProjectPageDto>>;
