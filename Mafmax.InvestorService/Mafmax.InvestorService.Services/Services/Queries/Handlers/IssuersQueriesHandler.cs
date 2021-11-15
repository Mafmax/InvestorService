using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Queries.Issuers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Services.Services.Queries.Handlers;

/// <summary>
/// Handle queries associated with issuers
/// </summary>
public class IssuersQueriesHandler : ServiceBase<InvestorDbContext>,
    IRequestHandler<GetIssuersQuery, IssuerDto[]>
{
    /// <inheritdoc />
    public IssuersQueriesHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper)
    {
    }

    /// <summary>
    /// Gets all issuers
    /// </summary>
    /// <param name="query"></param>
    /// <param name="token"></param>
    /// <returns>Array of issuers</returns>
    public async Task<IssuerDto[]> Handle(GetIssuersQuery query, CancellationToken token)
        => await Db.Issuers
            .Include(x => x.Country)
            .Include(x => x.Industry)
            .ProjectTo<IssuerDto>(Mapper.ConfigurationProvider)
            .ToArrayAsync(token);

}