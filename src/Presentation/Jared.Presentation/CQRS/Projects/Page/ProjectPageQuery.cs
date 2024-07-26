using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using MediatR;

namespace Jared.Presentation.CQRS.Projects.Page;

public sealed record ProjectPageQuery(Query query) : IRequest<Result<ProjectPageDto>>;
