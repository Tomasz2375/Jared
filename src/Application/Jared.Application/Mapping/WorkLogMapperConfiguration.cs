using Jared.Domain.Model;
using Jared.Shared.Dtos.WorkLogDtos;
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
    }
}
