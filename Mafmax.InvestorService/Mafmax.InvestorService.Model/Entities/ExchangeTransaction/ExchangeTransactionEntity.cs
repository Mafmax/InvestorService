using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Interfaces;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Mafmax.InvestorService.Model.Entities.ExchangeTransaction;

/// <summary>
/// Transaction entity
/// </summary>
public class ExchangeTransactionEntity: IHasId<int>
{

    /// <summary>
    /// <inheritdoc cref="ExchangeTransactionEntity"/>
    /// </summary>
    protected ExchangeTransactionEntity()
    {
        
    }

    /// <summary>
    /// <inheritdoc cref="ExchangeTransactionEntity"/>
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="lotsCount"></param>
    /// <param name="oneLotPrice"></param>
    /// <param name="type"></param>
    public ExchangeTransactionEntity(AssetEntity asset, int lotsCount, decimal oneLotPrice, ExchangeTransactionType type)
    {
        Asset = asset;
        LotsCount = lotsCount;
        OneLotPrice = oneLotPrice;
        Type = type;
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; protected set; }

    /// <summary>
    /// One lot of asset price
    /// </summary>
    [Column(TypeName = "decimal(10,6)")]
    public decimal OneLotPrice { get; protected set; }

    /// <summary>
    /// Asset entity
    /// </summary>
    public AssetEntity Asset { get; protected set; } = null!;

    /// <summary>
    /// <inheritdoc cref="ExchangeTransactionType"/>
    /// </summary>
    public ExchangeTransactionType Type { get; protected set; }

    /// <summary>
    /// Count of lots in one transaction
    /// </summary>
    public int LotsCount { get; protected set; }
}