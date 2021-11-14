
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Investors;

/// <summary>
/// Query to get investor id by login
/// </summary>
public record GetInvestorIdByLogin(string Login) : IRequest<int>;