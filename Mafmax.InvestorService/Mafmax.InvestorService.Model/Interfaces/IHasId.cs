using System;

namespace Mafmax.InvestorService.Model.Interfaces;

/// <summary>
/// Defines property Id
/// </summary>
/// <typeparam name="TKey">Type of Id</typeparam>
public interface IHasId<out TKey> where TKey : IEquatable<TKey>
{

    /// <summary>
    /// Identifier
    /// </summary>
    TKey? Id { get; }
}
