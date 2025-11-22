using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Tasks.Create;

public sealed record TaskCreateCommand(TaskDetailsDto dto) : IRequest<Result<bool>>;
