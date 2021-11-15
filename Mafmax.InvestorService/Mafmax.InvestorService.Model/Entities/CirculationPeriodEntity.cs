using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Mafmax.InvestorService.Model.Entities;

/// <summary>
/// Period of asset circulation
/// </summary>
[Owned]
public class CirculationPeriodEntity
{
    /// <summary>
    /// <inheritdoc cref="CirculationPeriodEntity"/>
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected CirculationPeriodEntity()
    {
    }

    /// <summary>
    /// <inheritdoc cref="CirculationPeriodEntity"/>
    /// </summary>
    public CirculationPeriodEntity(DateTime start)
    {
        Start = start;
    }

    /// <summary>
    /// <inheritdoc cref="CirculationPeriodEntity"/>
    /// </summary>
    public CirculationPeriodEntity(DateTime start, DateTime? end) : this()
    {
        Start = start;
        End = end;
    }

    /// <summary>
    /// Start date of circulation
    /// </summary>
    [Column("StartCirculation")]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public DateTime Start { get; protected set; }

    /// <summary>
    /// End date of circulation
    /// </summary>
    [Column("EndCirculation")]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public DateTime? End { get; protected set; }

}