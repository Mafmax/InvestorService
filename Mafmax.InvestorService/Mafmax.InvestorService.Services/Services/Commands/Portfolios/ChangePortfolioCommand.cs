using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Commands.Portfolios;

/// <summary>
/// Command to change portfolio parameters. 
/// </summary>
public record ChangePortfolioCommand(int InvestorId,
    int PortfolioId,
    string? NewName,
    string? NewTargetDescription) : IRequest<PortfolioDetailedInfoDto>;