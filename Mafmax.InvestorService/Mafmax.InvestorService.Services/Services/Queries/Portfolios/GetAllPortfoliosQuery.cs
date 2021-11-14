using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Portfolios;

/// <summary>
/// Query to get all portfolios of investor
/// </summary>
public record GetAllPortfoliosQuery(int InvestorId) : IRequest<PortfolioShortInfoDto[]>;