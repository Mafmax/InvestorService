using Mafmax.InvestorService.Services.Services.Queries.Interfaces;

namespace Mafmax.InvestorService.Services.Services.Queries.Investors;

/// <summary>
/// Query to get investor id by login
/// </summary>
public record GetInvestorIdByLogin(string Login) : IQuery<int>;