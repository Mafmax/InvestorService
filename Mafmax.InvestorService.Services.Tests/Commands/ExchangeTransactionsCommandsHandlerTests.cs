﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;
using Mafmax.InvestorService.Services.Services.Commands.Handlers;
using Mafmax.InvestorService.Services.Tests.Commands.Base;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Mafmax.InvestorService.Services.Tests.Commands;

public class ExchangeTransactionsCommandsHandlerTests : InvestorServiceCommandsHandlerTestsBase<ExchangeTransactionCommandsHandler>
{
    protected override ExchangeTransactionCommandsHandler GetHandler(Guid token) => 
        new(GetDb(token), Mapper);

    [Theory]
    [InlineData(777, 1, 1)]
    [InlineData(1, 777, 1)]
    [InlineData(1, 1, 777)]
    public async Task RemoveExchangeTransaction_ShouldThrows_IfInvestorOrPortfolioOrTransactionNotFound(
        int investorId, int portfolioId, int transactionId)
    {
        //Arrange
        RemoveExchangeTransactionCommand command = new(investorId, portfolioId, transactionId);

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Handler.ExecuteAsync(command));
    }

    [Fact]
    public async Task RemoveExchangeTransaction_ShouldRemovesTransaction()
    {
        //Arrange
        var token = GetDbToken();
        int startCount;
        int actualDifference;
        await using (var db = GetDb(token))
            startCount = db.ExchangeTransactions.Count();
        RemoveExchangeTransactionCommand command = new(1, 1, 1);

        //Act
        await GetHandler(token).ExecuteAsync(command);
        await using (var db = GetDb(token))
            actualDifference = startCount - db.ExchangeTransactions.Count();

        //Assert
        Assert.Equal(expected: 1, actualDifference);
    }

    [Theory]
    [InlineData(-5, 1)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    public async Task AddExchangeTransaction_ShouldThrows_IfDataIncorrect(decimal price, int lotsCount)
    {
        //Arrange
        AddExchangeTransactionCommand command =
            new(1, 1, 1, true, price, lotsCount);

        //Act

        //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await Handler.ExecuteAsync(command));
    }

    [Fact]
    public async Task AddExchangeTransaction_ShouldAddTransaction()
    {
        //Arrange
        var token = GetDbToken();
        int startCount;
        int actualDifference;

        await using (var db = GetDb(token))
            startCount =await db.ExchangeTransactions.CountAsync();
            
        AddExchangeTransactionCommand command =
            new(1, 1, 1, true, 100, 50);

        //Act
        await GetHandler(token).ExecuteAsync(command);

        await using (var db = GetDb(token))
            actualDifference =await db.ExchangeTransactions.CountAsync() - startCount;

        //Assert
        Assert.Equal(expected: 1, actualDifference);
    }
}