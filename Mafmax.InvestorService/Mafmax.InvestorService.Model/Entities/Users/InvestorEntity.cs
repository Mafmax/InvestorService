using System.Collections.Generic;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Mafmax.InvestorService.Model.Entities.Users;

/// <summary>
/// User who is investor
/// </summary>
public class InvestorEntity : UserEntity
{

    /// <summary>
    /// Collection of investment portfolios of investor
    /// </summary>
    public ICollection<InvestmentPortfolioEntity> Portfolios { get; set; } = new List<InvestmentPortfolioEntity>();
}