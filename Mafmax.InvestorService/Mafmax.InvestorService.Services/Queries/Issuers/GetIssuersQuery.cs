using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Queries.Issuers;

/// <summary>
/// Query to get all issuers
/// </summary>
public record GetIssuersQuery : IRequest<IssuerDto[]>;