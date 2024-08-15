using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Projects.List;

public sealed record ProjectListQuery : IRequest<Result<List<ProjectListDto>>>;
