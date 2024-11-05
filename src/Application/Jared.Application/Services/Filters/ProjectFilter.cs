using Jared.Domain.Models;
using Jared.Shared.Dtos.ProjectDtos;

namespace Jared.Application.Services.Filters;

public class ProjectFilter : IFilterStrategy<Project>
{
    public IQueryable<Project> ApplyFilter(IQueryable<Project> query, string key, string value)
    {
        if (filters.TryGetValue(key, out var filter))
        {
            query = filter(query, value);
        }

        return query;
    }

    private readonly Dictionary<string, Func<IQueryable<Project>, string, IQueryable<Project>>> filters = new()
    {
        {
            nameof(ProjectListDto.Id),
            (projects, value) => projects.Where(x => x.Id.ToString().Contains(value))
        },
        {
            nameof(ProjectListDto.Title),
            (projects, value) => projects.Where(x => x.Title.Contains(value))
        },
        {
            nameof(ProjectListDto.Code),
            (projects, value) => projects.Where(x => x.Code.Contains(value))
        },
    };
}
