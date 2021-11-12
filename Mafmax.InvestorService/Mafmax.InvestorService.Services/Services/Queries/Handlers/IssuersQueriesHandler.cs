using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Issuers;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Services.Services.Queries.Handlers;

/// <summary>
/// Handle queries associated with issuers
/// </summary>
public class IssuersQueriesHandler : ServiceBase<InvestorDbContext>,
    IQueryHandler<GetIssuersQuery, IssuerDto[]>
{
    /// <inheritdoc />
    public IssuersQueriesHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper)
    {
    }

    /// <summary>
    /// Gets all issuers
    /// </summary>
    /// <param name="query"></param>
    /// <returns>Array of issuers</returns>
    public async Task<IssuerDto[]> AskAsync(GetIssuersQuery query)
        => await Db.Issuers
            .Include(x => x.Country)
            .Include(x => x.Industry)
            .Select(x => Mapper.Map<IssuerDto>(x))
            .ToArrayAsync();

}