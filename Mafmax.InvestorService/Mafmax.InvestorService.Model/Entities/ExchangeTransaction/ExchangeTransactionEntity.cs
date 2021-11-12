using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mafmax.InvestorService.Model.Entities.Assets;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Mafmax.InvestorService.Model.Entities.ExchangeTransaction;

/// <summary>
/// Transaction entity
/// </summary>
public class ExchangeTransactionEntity
{
    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// One lot of asset price
    /// </summary>
    [Column(TypeName = "decimal(10,6)")]
    [Required]
    public decimal OneLotPrice { get; set; }

    /// <summary>
    /// Asset entity
    /// </summary>
    public AssetEntity Asset { get; set; }= null!;

    /// <summary>
    /// <inheritdoc cref="ExchangeTransactionType"/>
    /// </summary>
    public ExchangeTransactionType Type { get; set; }

    /// <summary>
    /// Count of lots in one transaction
    /// </summary>
    [Required]
    public int LotsCount { get; set; }
}