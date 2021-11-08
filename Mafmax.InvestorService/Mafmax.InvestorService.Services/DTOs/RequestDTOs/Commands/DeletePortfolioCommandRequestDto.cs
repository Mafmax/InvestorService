using Mafmax.InvestorService.Services.Services.Commands.Portfolios;

namespace Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands
{

    /// <summary>
    /// DTO to receive user data from request for <see cref="DeletePortfolioCommand"/>
    /// </summary>
    public record DeletePortfolioCommandRequestDto(int PortfolioId)
    {

        /// <summary>
        /// Creates command (<inheritdoc cref="DeletePortfolioCommand"/>) from DTO
        /// </summary>
        /// <param name="investorId">Investor id</param>
        /// <returns>Instance of <see cref="DeletePortfolioCommand"/></returns>
        public DeletePortfolioCommand GetCommand(int investorId)
        {
            return new(investorId, PortfolioId);
        }
    }
}