using Jared.Core.Abstractions;
using Jared.Dtos.WorkLogs;
using MediatR;

namespace Jared.Contracts.Worklogs;

public sealed record WorkLogStatisticsQuery(int userId, int month, int year)
    : IRequest<Result<List<WorkLogStatisticsDto>>>;
