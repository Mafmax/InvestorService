using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Model.Interfaces;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Mafmax.InvestorService.Model.Entities;

/// <summary>
/// Portfolio with transactions
/// </summary>
public class InvestmentPortfolioEntity : IHasId<int>
{

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Display name
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of portfolio goal
    /// </summary>
    [MinLength(10)]
    [MaxLength(500)]
    [Required]
    public string TargetDescription { get; set; } = string.Empty;

    /// <summary>
    /// Collection of <inheritdoc cref="ExchangeTransactionEntity"/>
    /// </summary>
    public ICollection<ExchangeTransactionEntity> Transactions { get; set; } = new List<ExchangeTransactionEntity>();
}