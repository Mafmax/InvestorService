using System;
using System.ComponentModel.DataAnnotations;
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
    public DateTime Start { get; protected set; }

    /// <summary>
    /// End date of circulation
    /// </summary>
    [Column("EndCirculation")]
    public DateTime? End { get; protected set; }

}