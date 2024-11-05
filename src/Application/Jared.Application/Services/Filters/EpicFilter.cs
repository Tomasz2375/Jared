using Jared.Domain.Models;
using Jared.Shared.Dtos.EpicDtos;

namespace Jared.Application.Services.Filters;

public class EpicFilter : IFilterStrategy<Epic>
{
    public IQueryable<Epic> ApplyFilter(IQueryable<Epic> query, string key, string value)
    {
        if (filters.TryGetValue(key, out var filter))
        {
            query = filter(query, value);
        }

        return query;
    }

    private readonly Dictionary<string, Func<IQueryable<Epic>, string, IQueryable<Epic>>> filters = new()
    {
        {
            nameof(EpicListDto.Id),
            (epics, value) => epics.Where(x => x.Id.ToString().Contains(value))
        },
        {
            nameof(EpicListDto.Title),
            (epics, value) => epics.Where(x => x.Title.Contains(value))
        },
        {
            nameof(EpicListDto.ParentId),
            (epics, value) => epics.Where(x => x.ParentId.ToString()!.Contains(value))
        },
        {
            nameof(EpicListDto.ProjectId),
            (epics, value) => epics.Where(x => x.ProjectId.ToString().Contains(value))
        },
        {
            nameof(EpicListDto.Status),
            (epics, value) =>
            {
                if (value == "0")
                {
                    return epics;
                }

                return epics.Where(x => ((int)x.Status & int.Parse(value)) != 0);
            }
        },
    };
}
