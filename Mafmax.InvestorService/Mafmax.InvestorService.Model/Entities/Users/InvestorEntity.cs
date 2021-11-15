using System.Collections.Generic;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Mafmax.InvestorService.Model.Entities.Users;

/// <summary>
/// User who is investor
/// </summary>
public class InvestorEntity : UserEntity
{
    /// <inheritdoc />
    public InvestorEntity(string login, byte[] passwordHash, List<InvestmentPortfolioEntity> portfolios)
    {
        Login = login;
        PasswordHash = passwordHash;
        Portfolios = portfolios;
    }

    /// <inheritdoc />
    public InvestorEntity(string login, byte[] passwordHash)
    {
        Login = login;
        PasswordHash = passwordHash;
    }

    /// <inheritdoc />
    protected InvestorEntity()
    {
        
    }

    /// <summary>
    /// Collection of investment portfolios of investor
    /// </summary>
    public List<InvestmentPortfolioEntity> Portfolios { get; protected set; } = new();
}