using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Services.Queries.Handlers;
using Mafmax.InvestorService.Services.Services.Queries.Issuers;
using Mafmax.InvestorService.Services.Tests.Queries.Base;
using Xunit;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Queries;

public class IssuersQueriesHandlerTests : InvestorServiceQueriesHandlerTestsBase<IssuersQueriesHandler>
{
    protected override IssuersQueriesHandler GetHandler()
    {
        var db = GetContext();
        var mapper = GetMapper(typeof(IssuersQueriesHandler).Assembly);
        return new(db, mapper);
    }

    [Fact]
    public async Task GetIssuers_ShouldReturnsResult()
    {
        //Arrange
        GetIssuersQuery query = new();

        //Act
        var issuers = await AskAsync(query);

        //Assert
        Assert.NotEmpty(issuers);
    }
}