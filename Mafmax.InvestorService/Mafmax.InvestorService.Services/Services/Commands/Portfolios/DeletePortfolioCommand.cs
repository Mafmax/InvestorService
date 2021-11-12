using Mafmax.InvestorService.Services.Services.Commands.Interfaces;

namespace Mafmax.InvestorService.Services.Services.Commands.Portfolios;

/// <summary>
/// Command to delete portfolio
/// </summary>
public record DeletePortfolioCommand(int InvestorId, int PortfolioId) : ICommand<int>;