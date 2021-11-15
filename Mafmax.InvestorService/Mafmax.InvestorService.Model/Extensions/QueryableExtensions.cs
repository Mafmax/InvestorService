using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Mafmax.InvestorService.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Model.Extensions;

/// <summary>
/// Extension methods for IQueryable`1 implementations
/// </summary>
public static class QueryableExtensions
{

    /// <summary>
    /// Finds value by id
    /// </summary>
    /// <typeparam name="TValue">Type of objects</typeparam>
    /// <typeparam name="TKey">Type of key</typeparam>
    /// <param name="query">Caller</param>
    /// <param name="id">Id to find</param>
    /// <param name="token">Token for cancel operation</param>
    /// <param name="idExprMsg">Sets by compiler</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async static Task<TValue?> ByIdAsync<TValue, TKey>(this IQueryable<TValue> query, TKey id, CancellationToken token, [CallerArgumentExpression("id")] string idExprMsg = null!)
        where TValue : IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        if (id is null) throw new ArgumentNullException(nameof(id), $"Expression was: <{idExprMsg}>");

        return await query.FirstOrDefaultAsync(x => id.Equals(x.Id), token);
    }
}
