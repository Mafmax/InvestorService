using System.ComponentModel.DataAnnotations;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedMember.Global

namespace Mafmax.InvestorService.Model.Entities;

/// <summary>
/// Company industry
/// </summary>
public class IndustryEntity
{
    /// <summary>
    /// <inheritdoc cref="IndustryEntity"/>
    /// </summary>
    protected IndustryEntity()
    {
    }

    /// <summary>
    /// <inheritdoc cref="IndustryEntity"/>
    /// </summary>
    public IndustryEntity(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Industry identifier
    /// </summary>
    [Key]
    public int Id { get; protected set; }

    /// <summary>
    /// Industry name
    /// </summary>
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public string Name { get; protected set; } = string.Empty;
}