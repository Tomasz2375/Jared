using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;

namespace Jared.Presentation.Requests.Projects.Page;

public sealed record ProjectPageQuery(Query query) : IRequest<Result<ProjectPageDto>>;
