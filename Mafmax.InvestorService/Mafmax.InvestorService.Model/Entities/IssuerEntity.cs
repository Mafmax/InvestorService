using System.ComponentModel.DataAnnotations;
using Mafmax.InvestorService.Model.Interfaces;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Mafmax.InvestorService.Model.Entities;

/// <summary>
/// Issuer entity
/// </summary>
public class IssuerEntity : IHasId<int>
{
    /// <summary>
    /// <inheritdoc cref="IssuerEntity"/>
    /// </summary>
    protected IssuerEntity()
    {
    }

    /// <summary>
    /// <inheritdoc cref="IssuerEntity"/>
    /// </summary>
   /* public IssuerEntity(IndustryEntity industry, CountryEntity country, string name)
    {
        Industry = industry;
        Country = country;
        Name = name;
    }*/
    public IssuerEntity( CountryEntity country, IndustryEntity industry, string name)
    {
        Industry = industry;
        Country = country;
        Name = name;
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; protected set; }

    /// <summary>
    /// Company name
    /// </summary>
    public string Name { get; protected set; } = string.Empty;

    /// <summary>
    /// Company country
    /// </summary>
    public CountryEntity Country { get; protected set; } = null!;

    /// <summary>
    /// Company industry
    /// </summary>
    public IndustryEntity Industry { get; protected set; } = null!;
}