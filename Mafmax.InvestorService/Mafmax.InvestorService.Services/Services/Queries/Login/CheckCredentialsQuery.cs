using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Login;

/// <summary>
/// Query to check credentials
/// </summary>
public record CheckCredentialsQuery(string Login, string Password) : IRequest<bool>;