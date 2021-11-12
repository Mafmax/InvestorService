using Mafmax.InvestorService.Services.Services.Queries.Portfolios;

namespace Mafmax.InvestorService.Services.DTOs.RequestDTOs.Queries;

/// <summary>
/// DTO to receive user data from request for <see cref="GetDetailedPortfolioQuery"/>
/// </summary>
public record GetDetailedPortfolioQueryRequestDto(int PortfolioId)
{

    /// <summary>
    /// Creates query (<inheritdoc cref="GetDetailedPortfolioQuery"/>) from DTO
    /// </summary>
    /// <param name="investorId">Investor id</param>
    /// <returns>Instance of <see cref="GetDetailedPortfolioQuery"/></returns>
    public GetDetailedPortfolioQuery GetQuery(int investorId)
    {
        return new(investorId, PortfolioId);
    }
}