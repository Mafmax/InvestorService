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
    /// <inheritdoc cref="InvestmentPortfolioEntity"/>
    /// </summary>
    protected InvestmentPortfolioEntity()
    {
        
    }

    /// <summary>
    /// <inheritdoc cref="InvestmentPortfolioEntity"/>
    /// </summary>
    public InvestmentPortfolioEntity(string name, string targetDescription, List<ExchangeTransactionEntity> transactions)
    {
        Name = name;
        TargetDescription = targetDescription;
        Transactions = transactions;
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Display name
    /// </summary>
    public string Name { get; protected set; } =string.Empty;

    /// <summary>
    /// Description of portfolio goal
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public string TargetDescription { get; protected set; } = string.Empty;

    /// <summary>
    /// Collection of <inheritdoc cref="ExchangeTransactionEntity"/>
    /// </summary>
    public List<ExchangeTransactionEntity> Transactions { get; set; } = new();

    /// <summary>
    /// Updates portfolio name
    /// </summary>
    /// <param name="newName"></param>
    public void UpdateName(string newName) => 
        Name = newName;

    /// <summary>
    /// Updates portfolio target description
    /// </summary>
    /// <param name="newTargetDescription"></param>
    public void UpdateTargetDescription(string newTargetDescription) => 
        TargetDescription = newTargetDescription;
}