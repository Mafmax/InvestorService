using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Commands.Portfolios;

/// <summary>
/// Command to create new portfolio
/// </summary>
public record CreatePortfolioCommand(int InvestorId,
    string Name, string TargetDescription, int PortfoliosCountLimit = 3) : IRequest<PortfolioDetailedInfoDto>;