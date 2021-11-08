using Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;

namespace Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands
{

    /// <summary>
    /// DTO to receive user data from request for <see cref="RemoveExchangeTransactionCommand"/>
    /// </summary>
    public record RemoveExchangeTransactionCommandRequestDto(int PortfolioId, int TransactionId)
    {

        /// <summary>
        /// Creates command (<inheritdoc cref="RemoveExchangeTransactionCommand"/>) from DTO
        /// </summary>
        /// <param name="investorId">Investor id</param>
        /// <returns>Instance of <see cref="RemoveExchangeTransactionCommand"/></returns>
        public RemoveExchangeTransactionCommand GetCommand(int investorId)
        {
            return new(investorId, PortfolioId, TransactionId);
        }
    }
}