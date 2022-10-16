using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Model.Extensions;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Queries.Portfolios;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;
using DistributionAggregate = System.ValueTuple<System.Collections.Generic.Dictionary<string, decimal>, decimal>;

namespace Mafmax.InvestorService.Services.Queries.Handlers;

/// <summary>
/// Handle queries associated with portfolio
/// </summary>
public class PortfolioQueriesHandler : ServiceBase<InvestorDbContext>,
    IRequestHandler<GetAllPortfoliosQuery, PortfolioShortInfoDto[]>,
    IRequestHandler<GetDetailedPortfolioQuery, PortfolioDetailedInfoDto>

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
    public async Task<PortfolioShortInfoDto[]> Handle(GetAllPortfoliosQuery query,CancellationToken token)
    {
        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .ByIdAsync(query.InvestorId,token);

        if (investor is null) ThrowEntityNotFound<InvestorEntity>(query.InvestorId);

        return investor.Portfolios
            .Select(x => Mapper.Map<PortfolioShortInfoDto>(x))
            .ToArray();
    }

    /// <summary>
    /// Gets detailed information of investor portfolio
    /// </summary>
    /// <param name="query"></param>
    /// <param name="token"></param>
    /// <returns>Detailed portfolio info</returns>
    /// <exception cref="EntityNotFoundException"/>
    public async Task<PortfolioDetailedInfoDto> Handle(GetDetailedPortfolioQuery query, CancellationToken token)
    {
        var (investorId, portfolioId) = query;

        var investor = await Db.Investors
            .Include(x => x.Portfolios).ThenInclude(x => x.Transactions).ThenInclude(x => x.Asset).ThenInclude(x => x.Stock)
            .Include(x => x.Portfolios).ThenInclude(x => x.Transactions).ThenInclude(x => x.Asset).ThenInclude(x => x.Issuer)
            .ByIdAsync(investorId,token);

        if (investor is null) ThrowEntityNotFound<InvestorEntity>(investorId);

        var portfolio = investor.Portfolios
            .ById(portfolioId);

        if (portfolio is null) ThrowEntityNotFound<InvestmentPortfolioEntity>(investorId);

        return FillDetailedPortfolioInfo(portfolio);
    }

    private PortfolioDetailedInfoDto FillDetailedPortfolioInfo(InvestmentPortfolioEntity portfolio)
    {
        var aggregationFunc = AggregationFunc;

        var (parts, totalPrice) = portfolio.Transactions
            .Aggregate(seed: (new(), 0), aggregationFunc);

        var distribution = parts
            .Select(x => (x.Key, x.Value / totalPrice * 100));

        return Mapper.Map<PortfolioDetailedInfoDto>(portfolio)
            with
        { AssetsDistribution = distribution.ToArray() };

        static DistributionAggregate AggregationFunc((Dictionary<string, decimal> Parts, decimal Total) accumulate, ExchangeTransactionEntity x)
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