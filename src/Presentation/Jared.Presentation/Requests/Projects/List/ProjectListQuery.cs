using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Projects.List;

public sealed record ProjectListQuery : IRequest<Result<List<ProjectListDto>>>;
