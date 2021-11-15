using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Services.Queries.Handlers;
using Mafmax.InvestorService.Services.Services.Queries.Login;
using Mafmax.InvestorService.Services.Tests.Queries.Base;
using Xunit;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Queries;

public class LoginQueriesHandlerTests :InvestorServiceQueriesHandlerTestsBase<LoginQueriesHandler>
{
    protected override LoginQueriesHandler GetHandler()
    {
        var db = GetContext();
            
        var mapper = GetMapper(typeof(LoginQueriesHandler).Assembly);

        return new(db, mapper);
    }

    [Theory]
    [InlineData("Investor1","1234aA",false)]
    [InlineData("Investor1", "qpeQPE772", true)]
    [InlineData("Investor2", "56772oOoO", true)]
    [InlineData("Investor4", "1234aA", false)]
    public async Task CheckCredentials_ShouldReturnsTrue_IfDataCorrect(string login, string password,bool expected)
    {
        //Arrange
        CheckCredentialsQuery query = new(login, password);

        //Act
        var actual = await AskAsync(query);

        //Assert
        Assert.Equal(expected,actual);
    }

    [Theory]
    [InlineData("Investor1",true)]
    [InlineData("Investor1000",false)]
    [InlineData("Investor2",true)]
    [InlineData("investor2",false)]
    [InlineData("Investor",false)]
    public async Task CheckLoginExists_ShouldReturnTrue_IfExists(string login, bool expected)
    {
        //Arrange
        CheckLoginExistsQuery query = new(login);

        //Act
        var actual = await AskAsync(query);

        //Assert
        Assert.Equal(expected, actual);
    }
}