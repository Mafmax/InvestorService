using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Services.Commands.Handlers;
using Mafmax.InvestorService.Services.Services.Commands.Login;
using Mafmax.InvestorService.Services.Tests.Commands.Base;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Mafmax.InvestorService.Services.Tests.Commands;

public class LoginCommandsHandlerTests : InvestorServiceCommandsHandlerTestsBase<LoginCommandsHandler>
{
    protected override LoginCommandsHandler GetHandler(Guid token) =>
        new(GetDb(token), Mapper);

    [Fact]
    public async Task RegisterUser_ShouldAddUser()
    {
        //Arrange
        var token = GetDbToken();
        int startCount;
        int actualDifference;

        await using (var db = GetDb(token))
            startCount =await db.Investors.CountAsync();

        RegisterInvestorCommand command = new("Investor4", "asdASD123");

        //Act
        await GetHandler(token).Handle(command,default);

        await using (var db = GetDb(token))
            actualDifference = await db.Investors.CountAsync() - startCount;

        //Assert
        Assert.Equal(expected: 1, actualDifference);
    }
}