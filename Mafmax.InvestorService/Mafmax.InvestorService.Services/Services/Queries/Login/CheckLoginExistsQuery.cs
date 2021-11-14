using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Login;

/// <summary>
/// Query to check login
/// </summary>
public record CheckLoginExistsQuery(string Login) : IRequest<bool>;