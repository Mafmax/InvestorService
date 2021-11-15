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
    /// <inheritdoc cref="StockExchangeEntity"/>
    /// </summary>
    public StockExchangeEntity()
    {
    }

    /// <summary>
    /// <inheritdoc cref="StockExchangeEntity"/>
    /// </summary>
    public StockExchangeEntity(string key, string name)
    {
        Key = key;
        Name = name;
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; protected set; }

    /// <summary>
    /// Stock exchange key e.g. MOEX
    /// </summary>
    public string Key { get; protected set; } = string.Empty;

    /// <summary>
    /// Stock exchange name
    /// </summary>
    public string Name { get; protected set; } = string.Empty;
}