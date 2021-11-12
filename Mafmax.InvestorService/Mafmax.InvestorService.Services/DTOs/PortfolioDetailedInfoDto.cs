using System.Collections.Generic;

namespace Mafmax.InvestorService.Services.DTOs;

/// <summary>
/// Full information DTO for <see cref="Mafmax.InvestorService.Model.Entities.InvestmentPortfolioEntity"/>
/// </summary>
public record PortfolioDetailedInfoDto(
    int Id,
    string Name,
    string TargetDescription,

    IEnumerable<ExchangeTransactionDto> Transactions)
{
    /// <summary>
    /// Distribution of assets (in percents)
    /// </summary>
    public IEnumerable<(string Name, decimal Part)> AssetsDistribution { get; init; } =
        new List<(string Name, decimal Part)>();
}