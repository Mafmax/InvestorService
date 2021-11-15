using Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;

namespace Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands;

/// <summary>
/// DTO to receive user data from request for <see cref="CreateExchangeTransactionCommand"/>
/// </summary>
public record AddExchangeTransactionCommandRequestDto(int PortfolioId,
    int AssetId,
    bool OrderToBuy,
    decimal OneLotPrice,
    int LotsCount)
{

    /// <summary>
    /// Creates command (<inheritdoc cref="CreateExchangeTransactionCommand"/>) from DTO
    /// </summary>
    /// <param name="investorId">Investor id</param>
    /// <returns>Instance of <see cref="CreateExchangeTransactionCommand"/></returns>
    public CreateExchangeTransactionCommand GetCommand(int investorId) =>
        new(investorId,
            PortfolioId,
            AssetId,
            OrderToBuy,
            OneLotPrice,
            LotsCount);
}