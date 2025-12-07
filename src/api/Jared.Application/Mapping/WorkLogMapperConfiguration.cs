using Jared.Domain.Models;
using Jared.Dtos.WorkLogs;
using Mapster;

namespace Jared.Application.Mapping;

public class WorkLogMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<WorkLog, WorkLogStatisticsDto>()
            .Map(d => d.ProjectId, s => s.Task!.ProjectId)
            .Map(d => d.ProjectTitle, s => s.Task!.Project!.Title)
            .Map(d => d.Color, s => s.Task!.Project!.Color);

        config
            .NewConfig<WorkLog, WorkLogListDto>()
            .Map(d => d.UserFullName, s => $"{s.User!.FirstName} {s.User.LastName}");
    }
}
