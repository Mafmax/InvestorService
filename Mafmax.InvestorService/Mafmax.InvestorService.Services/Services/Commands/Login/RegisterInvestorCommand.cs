using Mafmax.InvestorService.Services.Services.Commands.Interfaces;

namespace Mafmax.InvestorService.Services.Services.Commands.Login
{

    /// <summary>
    /// Command to register new investor
    /// </summary>
    public record RegisterInvestorCommand(string Login, string Password) 
        : ICommand<int>;
}