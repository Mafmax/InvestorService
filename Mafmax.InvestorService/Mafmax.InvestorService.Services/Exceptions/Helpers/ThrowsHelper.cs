using System.Diagnostics.CodeAnalysis;

namespace Mafmax.InvestorService.Services.Exceptions.Helpers;

/// <summary>
/// Simplifies work with exceptions throwing
/// </summary>
public static class ThrowsHelper
{

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// </summary>
    /// <typeparam name="TEntity">Not found entity type</typeparam>
    /// <param name="param">Parameter associated with entity</param>
    /// <exception cref="EntityNotFoundException"></exception>
    [DoesNotReturn]
    public static void ThrowEntityNotFound<TEntity>(object param) =>
        throw new EntityNotFoundException($"{typeof(TEntity).Name} with param <{param}> not found");

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// </summary>
    /// <typeparam name="TWho">Not found entity type</typeparam>
    /// <typeparam name="TWhom">Entity which not contains finding type</typeparam>
    /// <param name="whoParam">Parameter associated with entity who includes</param>
    /// <param name="whomParam">Param associated with entity whom included</param>
    /// <exception cref="EntityNotFoundException"></exception>
    [DoesNotReturn]
    public static void ThrowIncludedEntityNotFound<TWho, TWhom>(object whoParam, object whomParam)
        => throw new EntityNotFoundException(
            $"{typeof(TWhom).Name} with param <{whomParam}> " +
            $"for the {typeof(TWho).Name} with param <{whoParam}> not found");
}