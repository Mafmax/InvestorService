using System;
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
        DeleteExchangeTransactionCommand command = new(investorId, portfolioId, transactionId);

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Execute(command));
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
        DeleteExchangeTransactionCommand command = new(1, 1, 1);

        //Act
        await GetHandler(token).Handle(command,default);
        await using (var db = GetDb(token))
            actualDifference = startCount - db.ExchangeTransactions.Count();

        //Assert
        Assert.Equal(expected: 1, actualDifference);
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
            
        CreateExchangeTransactionCommand command =
            new(1, 1, 1, true, 100, 50);

        //Act
        await GetHandler(token).Handle(command,default);

        await using (var db = GetDb(token))
            actualDifference =await db.ExchangeTransactions.CountAsync() - startCount;

        //Assert
        Assert.Equal(expected: 1, actualDifference);
    }
}