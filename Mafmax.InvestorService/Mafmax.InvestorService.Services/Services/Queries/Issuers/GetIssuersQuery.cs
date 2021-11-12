using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;

namespace Mafmax.InvestorService.Services.Services.Queries.Issuers;

/// <summary>
/// Query to get all issuers
/// </summary>
public record GetIssuersQuery : IQuery<IssuerDto[]>;