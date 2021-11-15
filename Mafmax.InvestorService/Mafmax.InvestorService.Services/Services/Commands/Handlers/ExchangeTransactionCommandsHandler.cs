using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Model.Extensions;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;

namespace Mafmax.InvestorService.Services.Services.Commands.Handlers;

/// <summary>
/// Handle commands associated with transactions
/// </summary>
public class ExchangeTransactionCommandsHandler : ServiceBase<InvestorDbContext>,
    IRequestHandler<DeleteExchangeTransactionCommand>,
    IRequestHandler<CreateExchangeTransactionCommand, ExchangeTransactionDto>

{
    /// <inheritdoc />
    public ExchangeTransactionCommandsHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Remove transaction from portfolio
    /// </summary>
    ///<exception cref="EntityNotFoundException"/>
    public async Task<Unit> Handle(DeleteExchangeTransactionCommand command, CancellationToken token)
    {
        var (investorId, portfolioId, transactionId) = command;

        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .ThenInclude(x => x.Transactions)
            .ByIdAsync(investorId, token);

        var portfolio = investor?.Portfolios
            .ById(portfolioId);

        var transaction = portfolio?.Transactions
            .ById(transactionId);

        Validate(investor, portfolio, transaction);

        Db.ExchangeTransactions.Remove(transaction);

        await Db.SaveChangesAsync(token);

        return Unit.Value;

        void Validate([NotNull] InvestorEntity? i,
            [NotNull] InvestmentPortfolioEntity? ip,
            [NotNull] ExchangeTransactionEntity? et)
        {
            if (i is null)
                ThrowEntityNotFound<InvestorEntity>(investorId);

            if (ip is null)
                ThrowIncludedEntityNotFound<InvestorEntity, InvestmentPortfolioEntity>(investorId, portfolioId);

            if (et is null)
                ThrowIncludedEntityNotFound<InvestmentPortfolioEntity, ExchangeTransactionEntity>(portfolioId, transactionId);
        }
    }

    /// <summary>
    /// Adds transaction to portfolio
    /// </summary>
    /// <returns>Created transaction</returns>
    ///<exception cref="EntityNotFoundException"/>
    public async Task<ExchangeTransactionDto> Handle(CreateExchangeTransactionCommand command, CancellationToken token)
    {
        var asset = await Db.Assets
            .Include(x => x.Stock)
            .ByIdAsync(command.AssetId, token);

        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .ByIdAsync(command.InvestorId, token);

        var portfolio = investor?.Portfolios
            .ById(command.PortfolioId);

        Validate(investor, asset, portfolio);

        var transaction = Mapper.Map<ExchangeTransactionEntity>(command);

        portfolio.Transactions.Add(transaction);

        await Db.SaveChangesAsync(token);

        return Mapper.Map<ExchangeTransactionDto>(transaction);

        void Validate([NotNull] InvestorEntity? i,
            [NotNull] AssetEntity? a,
            [NotNull] InvestmentPortfolioEntity? ip)
        {
            if (i is null) ThrowEntityNotFound<InvestorEntity>(command.InvestorId);

            if (a is null) ThrowEntityNotFound<AssetEntity>(command.AssetId);

            if (ip is null) ThrowEntityNotFound<InvestmentPortfolioEntity>(command.PortfolioId);
        }
    }

}