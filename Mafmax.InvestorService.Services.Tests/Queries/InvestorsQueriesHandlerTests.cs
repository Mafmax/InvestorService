using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Queries.Handlers;
using Mafmax.InvestorService.Services.Services.Queries.Investors;
using Mafmax.InvestorService.Services.Tests.Queries.Base;
using Xunit;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Queries;

public class InvestorsQueriesHandlerTests :InvestorServiceQueriesHandlerTestsBase<InvestorsQueriesHandler>
{
    protected override InvestorsQueriesHandler GetHandler()
    {
        var db = GetContext("Readonly");

        var mapper = GetMapper(typeof(InvestorsQueriesHandler).Assembly);

        return new(db, mapper);
    }

    [Theory]
    [InlineData("Investor")]
    [InlineData("Investor4")]
    public async Task GetInvestorIdByLogin_ShouldThrows_IfInvestorNotFound(string login)
    {
        //Arrange
        GetInvestorIdByLogin query = new(login);

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Handler.AskAsync(query));
    }

    [Theory]
    [InlineData("Investor1",1)]
    [InlineData("Investor2",2)]
    public async void GetInvestorIdByLogin_ShouldReturns_IfInvestorFound(string login, int id)
    {
        //Arrange
        GetInvestorIdByLogin query = new(login);

        //Act
        var actualId= await GetHandler().AskAsync(query);

        //Assert
        Assert.Equal(id,actualId);
    }
}