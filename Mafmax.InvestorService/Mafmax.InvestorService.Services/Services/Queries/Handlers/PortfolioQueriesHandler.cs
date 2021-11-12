using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Portfolios;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;
using DistributionAggregate = System.ValueTuple<System.Collections.Generic.Dictionary<string, decimal>, decimal>;

namespace Mafmax.InvestorService.Services.Services.Queries.Handlers;

/// <summary>
/// Handle queries associated with portfolio
/// </summary>
public class PortfolioQueriesHandler : ServiceBase<InvestorDbContext>,
    IQueryHandler<GetAllPortfoliosQuery, PortfolioShortInfoDto[]>,
    IQueryHandler<GetDetailedPortfolioQuery, PortfolioDetailedInfoDto>

{
    /// <inheritdoc />
    public PortfolioQueriesHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper)
    {
    }

    /// <summary>
    /// Gets all portfolios of investor
    /// </summary>
    /// <returns>Array of portfolios found</returns>
    /// <exception cref="EntityNotFoundException"/>
    public async Task<PortfolioShortInfoDto[]> AskAsync(GetAllPortfoliosQuery query)
    {
        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .FirstOrDefaultAsync(x => x.Id.Equals(query.InvestorId));

        if (investor is null) ThrowEntityNotFound<InvestorEntity>(query.InvestorId);

        return investor!.Portfolios
            .Select(x => Mapper.Map<PortfolioShortInfoDto>(x))
            .ToArray();
    }

    /// <summary>
    /// Gets detailed information of investor portfolio
    /// </summary>
    /// <param name="query"></param>
    /// <returns>Detailed portfolio info</returns>
    /// <exception cref="EntityNotFoundException"/>
    public async Task<PortfolioDetailedInfoDto> AskAsync(GetDetailedPortfolioQuery query)
    {
        var investor = await Db.Investors
            .Include(x => x.Portfolios).ThenInclude(x => x.Transactions).ThenInclude(x => x.Asset).ThenInclude(x => x.Stock)
            .Include(x => x.Portfolios).ThenInclude(x => x.Transactions).ThenInclude(x => x.Asset).ThenInclude(x => x.Issuer)
            .FirstOrDefaultAsync(x => x.Id.Equals(query.InvestorId));

        if (investor is null) ThrowEntityNotFound<InvestorEntity>(query.InvestorId);

        var portfolio = investor?.Portfolios
            .FirstOrDefault(x => x.Id.Equals(query.PortfolioId));

        if (portfolio is null) ThrowEntityNotFound<InvestmentPortfolioEntity>(query.InvestorId);

        return FillDetailedPortfolioInfo(portfolio!);
    }

    private PortfolioDetailedInfoDto FillDetailedPortfolioInfo(InvestmentPortfolioEntity portfolio)
    {
        var aggregationFunc = AggregationFunc;

        var aggregate = portfolio.Transactions
            .Aggregate((new(), 0), aggregationFunc);

        var distribution = aggregate.Item1
            .Select(x => (x.Key, x.Value / aggregate.Item2 * 100));

        return Mapper.Map<PortfolioDetailedInfoDto>(portfolio)
            with
        { AssetsDistribution = distribution.ToArray() };

        static (Dictionary<string, decimal>, decimal) AggregationFunc((Dictionary<string, decimal> Parts, decimal Total) accumulate, ExchangeTransactionEntity x)
        {
            var sum = x.LotsCount * x.OneLotPrice;

            var assetClassName = x.Asset.Class;

            if (accumulate.Parts.ContainsKey(assetClassName))
                accumulate.Parts[x.Asset.Class] += sum;
            else
                accumulate.Parts.Add(assetClassName, sum);

            accumulate.Total += sum;

            return accumulate;
        }
    }
}