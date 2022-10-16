using Mafmax.InvestorService.Services.Commands.ExchangeTransactions;

namespace Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands;

/// <summary>
/// DTO to receive user data from request for <see cref="DeleteExchangeTransactionCommand"/>
/// </summary>
public record RemoveExchangeTransactionCommandRequestDto(int PortfolioId, int TransactionId)
{

    /// <summary>
    /// Creates command (<inheritdoc cref="DeleteExchangeTransactionCommand"/>) from DTO
    /// </summary>
    /// <param name="investorId">Investor id</param>
    /// <returns>Instance of <see cref="DeleteExchangeTransactionCommand"/></returns>
    public DeleteExchangeTransactionCommand GetCommand(int investorId)
    {
        return new(investorId, PortfolioId, TransactionId);
    }
}