using Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;

namespace Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands;

/// <summary>
/// DTO to receive user data from request for <see cref="AddExchangeTransactionCommand"/>
/// </summary>
public record AddExchangeTransactionCommandRequestDto(int PortfolioId,
    int AssetId,
    bool OrderToBuy,
    decimal OneLotPrice,
    int LotsCount)
{

    /// <summary>
    /// Creates command (<inheritdoc cref="AddExchangeTransactionCommand"/>) from DTO
    /// </summary>
    /// <param name="investorId">Investor id</param>
    /// <returns>Instance of <see cref="AddExchangeTransactionCommand"/></returns>
    public AddExchangeTransactionCommand GetCommand(int investorId)
    {
        return new AddExchangeTransactionCommand(InvestorId: investorId,
            PortfolioId,
            AssetId,
            OrderToBuy,
            OneLotPrice,
            LotsCount);
    }
}