using System.ComponentModel.DataAnnotations;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedMember.Global

namespace Mafmax.InvestorService.Model.Entities;

/// <summary>
/// Country entity
/// </summary>
public class CountryEntity
{
    /// <summary>
    /// <inheritdoc cref="CountryEntity"/>
    /// </summary>
    protected CountryEntity()
    {
    }

    /// <summary>
    /// <inheritdoc cref="CountryEntity"/>
    /// </summary>
    public CountryEntity(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; protected set; } 

    /// <summary>
    /// Country name
    /// </summary>
    public string Name { get; protected set; } = string.Empty;
}