using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Services.Services.Queries.Investors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;

namespace Mafmax.InvestorService.Services.Services.Queries.Handlers;

/// <summary>
/// Handle queries associated with investor
/// </summary>
public class InvestorsQueriesHandler : ServiceBase<InvestorDbContext>,
    IRequestHandler<GetInvestorIdByLogin, int>
{
    /// <inheritdoc />
    public InvestorsQueriesHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Get investor id by login
    /// </summary>
    /// <returns>Investor id</returns>
    public async Task<int> Handle(GetInvestorIdByLogin query, CancellationToken token)
    {
        var investor = await Db.Investors
            .FirstOrDefaultAsync(x => x.Login.Equals(query.Login), token);

        if (investor is null) ThrowEntityNotFound<InvestorEntity>(query.Login);

        return investor.Id;
    }
}