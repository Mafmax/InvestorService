using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Mafmax.InvestorService.Model.Interfaces;

namespace Mafmax.InvestorService.Model.Extensions;

/// <summary>
/// Extenstion method for IEnumerable`1 implementations
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Finds value by id
    /// </summary>
    /// <typeparam name="TValue">Type of objects</typeparam>
    /// <typeparam name="TKey">Type of key</typeparam>
    /// <param name="collection">Caller</param>
    /// <param name="id">Id to find</param>
    /// <param name="idExprMsg">Sets by compiler</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static TValue? ById<TValue, TKey>(this IEnumerable<TValue> collection, TKey id, [CallerArgumentExpression("id")] string idExprMsg = null!)
        where TValue : IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        if (id is null) throw new ArgumentNullException(nameof(id), $"Expression was: <{idExprMsg}>");

        return collection.FirstOrDefault(x => id.Equals(x.Id));
    }
}
