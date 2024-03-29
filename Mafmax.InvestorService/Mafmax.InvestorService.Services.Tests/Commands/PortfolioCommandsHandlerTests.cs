﻿using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Commands.Handlers;
using Mafmax.InvestorService.Services.Commands.Portfolios;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Tests.Commands.Base;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Mafmax.InvestorService.Services.Tests.Commands;

public class PortfolioCommandsHandlerTests : InvestorServiceCommandsHandlerTestsBase<PortfolioCommandsHandler>
{
    protected override PortfolioCommandsHandler GetHandler(Guid token) =>
        new(GetDb(token), Mapper);

   
    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 1)]
    public async Task CreatePortfolio_ShouldThrow_IfLimitReached(int investorId, int limit)
    {
        //Arrange
        CreatePortfolioCommand cmd = new(investorId,
            "Some name", "Some target description", limit);

        //Act

        //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await Execute(cmd));
    }

    [Fact]
    public async Task CreatePortfolio_ShouldCreatePortfolio()
    {
        //Arrange
        var token = GetDbToken();
        int startCount;
        int actualDifference;

        await using (var db = GetDb(token))
            startCount = await db.InvestmentPortfolios.CountAsync();

        CreatePortfolioCommand command = new(2,
            "Some name", "Some target description");

        //Act
        await GetHandler(token).Handle(command,default);

        await using (var db = GetDb(token))
            actualDifference = await db.InvestmentPortfolios.CountAsync() - startCount;

        //Assert
        Assert.Equal(expected: 1, actualDifference);
    }

    [Fact]
    public async Task ChangePortfolio_ShouldNotChangePortfoliosCount()
    {
        //Arrange
        var token = GetDbToken();
        int startCount;
        int actualDifference;

        await using (var db = GetDb(token))
            startCount = await db.InvestmentPortfolios.CountAsync();

        UpdatePortfolioCommand command = new(2, 4,
            "Some name", "Some target description");

        //Act
        await GetHandler(token).Handle(command,default);

        await using (var db = GetDb(token))
            actualDifference = await db.InvestmentPortfolios.CountAsync() - startCount;

        //Assert
        Assert.Equal(expected: 0, actualDifference);
    }

    [Fact]
    public async Task RemovePortfolio_ShouldRemovesPortfolio()
    {
        //Arrange
        var token = GetDbToken();
        int startCount;
        int actualDifference;
        await using (var db = GetDb(token))
            startCount =await db.InvestmentPortfolios.CountAsync();
        DeletePortfolioCommand command = new(1, 1);

        //Act
        await GetHandler(token).Handle(command,default);

        await using (var db = GetDb(token))
            actualDifference = startCount - await db.InvestmentPortfolios.CountAsync();

        //Assert
        Assert.Equal(expected: 1, actualDifference);
    }

    [Theory]
    [InlineData(1, 4)]
    [InlineData(2, 2)]
    [InlineData(3, 1)]
    public async Task RemovePortfolio_ShouldThrows_IfNotFound(int investorId, int portfolioId)
    {
        //Arrange
        DeletePortfolioCommand command = new(investorId, portfolioId);

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(
            async () => await Execute(command));
    }

    [Fact]
    public async Task RemovePortfolio_ShouldThrows_IfLast()
    {
        //Arrange
        DeletePortfolioCommand command = new(2, 4);

        //Act

        //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await Execute(command));
    }
}