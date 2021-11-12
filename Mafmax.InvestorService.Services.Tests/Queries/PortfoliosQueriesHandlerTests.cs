using System.Linq;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Queries.Handlers;
using Mafmax.InvestorService.Services.Services.Queries.Portfolios;
using Mafmax.InvestorService.Services.Tests.Queries.Base;
using Xunit;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;
namespace Mafmax.InvestorService.Services.Tests.Queries;

public class PortfoliosQueriesHandlerTests : InvestorServiceQueriesHandlerTestsBase<PortfolioQueriesHandler>
{
    protected override PortfolioQueriesHandler GetHandler()
    {
        var db = GetContext("Readonly");

        var mapper = GetMapper(typeof(PortfolioQueriesHandler).Assembly);

        return new(db, mapper);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    public async Task GetAllPortfolios_ShouldThrows_IfInvestorNotFound(int id)
    {
        //Arrange
        GetAllPortfoliosQuery query = new(id);

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Handler.AskAsync(query));
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 1)]
    public async Task GetAllPortfolios_ShouldReturnsValuesOfAllPortfolios(int id, int expected)
    {
        //Arrange
        GetAllPortfoliosQuery query = new(id);

        //Act
        var portfolios = await Handler.AskAsync(query);
        var actual = portfolios.Length;

        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetDetailedPortfolio_ShouldReturnsValue_WithAllIncludes()
    {
        //Arrange
        GetDetailedPortfolioQuery query = new(InvestorId: 1, PortfolioId: 1);

        //Act
        var portfolio = await Handler.AskAsync(query);
        var asset = portfolio.Transactions.First().Asset;

        //Assert
        Assert.NotNull(portfolio.Transactions);
        Assert.NotNull(portfolio.AssetsDistribution);
        Assert.NotNull(asset);
        Assert.NotNull(asset.Circulation);
        Assert.NotNull(asset.Issuer);
        Assert.NotNull(asset.Stock);
    }

    [Theory]
    [InlineData(1, 4)]
    [InlineData(5, 1)]
    public async Task GetDetailedPortfolio_ShouldThrows_IfInvestorOrPortfolioNotFound(int investorId, int portfolioId)
    {
        //Arrange
        GetDetailedPortfolioQuery query = new(investorId, portfolioId);

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Handler.AskAsync(query));
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 3)]
    [InlineData(2, 4)]
    public async Task GetDetailedPortfolio_ShouldReturnsCorrectDistribution(int investorId, int portfolioId)
    {
        //Arrange
        GetDetailedPortfolioQuery query = new(investorId, portfolioId);

        //Act
        var portfolio = await Handler.AskAsync(query);
        var actual = portfolio.AssetsDistribution.Aggregate(0m, (total, x) => total + x.Part);

        //Assert
        Assert.Equal(expected: 100m, actual);
    }
}