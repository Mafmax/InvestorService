using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;

/// <summary>
/// Command to add transaction into portfolio
/// </summary>
public record AddExchangeTransactionCommand(int InvestorId,
    int PortfolioId,
    int AssetId,
    bool OrderToBuy, 
    decimal OneLotPrice, 
    int LotsCount) : IRequest<ExchangeTransactionDto>;