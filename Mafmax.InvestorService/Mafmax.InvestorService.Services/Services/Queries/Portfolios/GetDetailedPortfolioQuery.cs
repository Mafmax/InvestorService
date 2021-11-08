using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;

namespace Mafmax.InvestorService.Services.Services.Queries.Portfolios
{

    /// <summary>
    /// Query to get detailed portfolio info from investor
    /// </summary>
    public record GetDetailedPortfolioQuery(int InvestorId, int PortfolioId) : IQuery<PortfolioDetailedInfoDto>;
}