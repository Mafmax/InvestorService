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
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Company name
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Company country
    /// </summary>
    public CountryEntity Country { get; set; } = null!;

    /// <summary>
    /// Company industry
    /// </summary>
    public IndustryEntity Industry { get; set; } = null!;
}