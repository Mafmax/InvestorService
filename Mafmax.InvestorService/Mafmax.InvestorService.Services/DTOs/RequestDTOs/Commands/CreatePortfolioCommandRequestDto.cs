using Mafmax.InvestorService.Services.Services.Commands.Portfolios;

namespace Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands;

/// <summary>
/// DTO to receive user data from request for <see cref="CreatePortfolioCommand"/>
/// </summary>
public record CreatePortfolioCommandRequestDto(string Name, string TargetDescription)
{

    /// <summary>
    /// Creates command (<inheritdoc cref="CreatePortfolioCommand"/>) from DTO
    /// </summary>
    /// <param name="investorId">Investor id</param>
    /// <param name="portfoliosCountLimit">Max count of portfolios for single investor</param>
    /// <returns>Instance of <see cref="CreatePortfolioCommand"/></returns>
    public CreatePortfolioCommand GetCommand(int investorId, int portfoliosCountLimit)
    {
        return new(investorId, Name, TargetDescription, portfoliosCountLimit);
    }
}