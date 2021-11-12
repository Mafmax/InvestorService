using System;

namespace Mafmax.InvestorService.Services.DTOs;

/// <summary>
/// DTO for <see cref="Mafmax.InvestorService.Model.Entities.CirculationPeriodEntity"/>
/// </summary>
public record CirculationPeriodDto(DateTime Start, DateTime? End);