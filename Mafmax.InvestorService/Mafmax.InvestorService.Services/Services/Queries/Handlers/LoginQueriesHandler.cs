using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Login;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Services.Services.Queries.Handlers;

/// <summary>
/// Handle queries associated with login
/// </summary>
public class LoginQueriesHandler : ServiceBase<InvestorDbContext>,
    IQueryHandler<CheckCredentialsQuery, bool>,
    IQueryHandler<CheckLoginExistsQuery, bool>
{
    /// <inheritdoc />
    public LoginQueriesHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Checks credentials
    /// </summary>
    /// <returns>True if credentials are valid</returns>
    public async Task<bool> AskAsync(CheckCredentialsQuery query)
    {
        var user = await Db.Users
            .FirstOrDefaultAsync(x => x.Login.Equals(query.Login));

        if (user is null) return false;

        var hash = SHA256.HashData(Encoding.Default.GetBytes(query.Password));

        return user.PasswordHash.SequenceEqual(hash);
    }

    /// <summary>
    /// Check login
    /// </summary>
    /// <param name="query"></param>
    /// <returns>True if login exists</returns>
    public async Task<bool> AskAsync(CheckLoginExistsQuery query) =>
        await Db.Users
            .FirstOrDefaultAsync(x => x.Login.Equals(query.Login)) is not null;
}