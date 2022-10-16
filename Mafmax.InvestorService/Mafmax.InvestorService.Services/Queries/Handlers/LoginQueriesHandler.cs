using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Services.Queries.Login;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Services.Queries.Handlers;

/// <summary>
/// Handle queries associated with login
/// </summary>
public class LoginQueriesHandler : ServiceBase<InvestorDbContext>,
    IRequestHandler<CheckCredentialsQuery, bool>,
    IRequestHandler<CheckLoginExistsQuery, bool>
{
    /// <inheritdoc />
    public LoginQueriesHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Checks credentials
    /// </summary>
    /// <returns>True if credentials are valid</returns>
    public async Task<bool> Handle(CheckCredentialsQuery query, CancellationToken token)
    {
        var (login, password) = query;

        var user = await Db.Users
            .FirstOrDefaultAsync(x => x.Login.Equals(login), token);

        if (user is null) return false;

        var hash = SHA256.HashData(Encoding.Default.GetBytes(password));

        return user.PasswordHash.SequenceEqual(hash);
    }

    /// <summary>
    /// Check login
    /// </summary>
    /// <returns>True if login exists</returns>
    public async Task<bool> Handle(CheckLoginExistsQuery query, CancellationToken token) =>
        await Db.Users
            .FirstOrDefaultAsync(x => x.Login.Equals(query.Login), token) is not null;
}