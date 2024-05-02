using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Queries.EpicQueries;

public class EpicDetailsQueryHandler : IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public EpicDetailsQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<EpicDetailsDto>> Handle(EpicDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var epic = await dataContext.Set<Epic>()
                .Include(x => x.Tasks)
                .FirstAsync(x => x.Id == request.id, cancellationToken);

            var result = mapper.Map<EpicDetailsDto>(epic);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }
}
