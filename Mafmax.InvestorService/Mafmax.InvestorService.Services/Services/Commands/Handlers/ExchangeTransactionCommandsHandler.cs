using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;
using Mafmax.InvestorService.Services.Services.Commands.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;

namespace Mafmax.InvestorService.Services.Services.Commands.Handlers;

/// <summary>
/// Handle commands associated with transactions
/// </summary>
public class ExchangeTransactionCommandsHandler : ServiceBase<InvestorDbContext>,
    ICommandHandler<RemoveExchangeTransactionCommand, int>,
    ICommandHandler<AddExchangeTransactionCommand, ExchangeTransactionDto>

{
    /// <inheritdoc />
    public ExchangeTransactionCommandsHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Remove transaction from portfolio
    /// </summary>
    ///<exception cref="EntityNotFoundException"/>
    public async Task<int> ExecuteAsync(RemoveExchangeTransactionCommand command)
    {
        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .ThenInclude(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.Id.Equals(command.InvestorId));

        var portfolio = investor?.Portfolios
            .FirstOrDefault(x => x.Id.Equals(command.PortfolioId));

        var transaction = portfolio?.Transactions
            .FirstOrDefault(x => x.Id.Equals(command.TransactionId));

        Validate(investor, portfolio, transaction);

        Db.ExchangeTransactions.Remove(transaction!);

        return await Db.SaveChangesAsync();

        void Validate(InvestorEntity? i, InvestmentPortfolioEntity? ip, ExchangeTransactionEntity? et)
        {
            if (i is null)
                ThrowEntityNotFound<InvestorEntity>(command.InvestorId);

            if (ip is null)
                ThrowIncludedEntityNotFound<InvestorEntity, InvestmentPortfolioEntity>(command.InvestorId, command.PortfolioId);

            if (et is null)
                ThrowIncludedEntityNotFound<InvestmentPortfolioEntity, ExchangeTransactionEntity>(command.PortfolioId, command.TransactionId);
        }
    }

    /// <summary>
    /// Adds transaction to portfolio
    /// </summary>
    /// <returns>Created transaction</returns>
    ///<exception cref="EntityNotFoundException"/>
    ///<exception cref="InvalidOperationException"/>
    public async Task<ExchangeTransactionDto> ExecuteAsync(AddExchangeTransactionCommand command)
    {
        var asset = await Db.Assets
            .Include(x=>x.Stock)
            .FirstOrDefaultAsync(x => x.Id.Equals(command.AssetId));

        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .FirstOrDefaultAsync(x => x.Id.Equals(command.InvestorId));

        var portfolio = investor?.Portfolios
            .FirstOrDefault(x => x.Id.Equals(command.PortfolioId));

        Validate(investor, asset, portfolio);

        var transaction = new ExchangeTransactionEntity()
        {
            Asset = asset,
            LotsCount = command.LotsCount,
            Type = command.OrderToBuy ? ExchangeTransactionType.Buy : ExchangeTransactionType.Sell,
            OneLotPrice = command.OneLotPrice
        };

        portfolio!.Transactions.Add(transaction);

        await Db.SaveChangesAsync();

        return Mapper.Map<ExchangeTransactionDto>(transaction);

        void Validate(InvestorEntity? i, AssetEntity? a, InvestmentPortfolioEntity? ip)
        {
            if (i is null) ThrowEntityNotFound<InvestorEntity>(command.InvestorId);

            if (a is null) ThrowEntityNotFound<AssetEntity>(command.AssetId);

            if (ip is null) ThrowEntityNotFound<InvestmentPortfolioEntity>(command.PortfolioId);

            if (command.LotsCount <= 0)
                throw new InvalidOperationException("LotsCount must be greater than 0");

            if (command.OneLotPrice <= 0)
                throw new InvalidOperationException("OneLotPrice must be greater than 0");

        }

    }
}