using System.ComponentModel.DataAnnotations;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Mafmax.InvestorService.Model.Entities;

/// <summary>
/// Stock exchange entity
/// </summary>
public class StockExchangeEntity
{

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Stock exchange key e.g. MOEX
    /// </summary>
    [Required]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Stock exchange name
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;
}