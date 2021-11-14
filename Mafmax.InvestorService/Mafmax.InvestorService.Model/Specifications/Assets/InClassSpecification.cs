using System;
using System.Linq.Expressions;
using LinqSpecs.Core;
using Mafmax.InvestorService.Model.Entities.Assets;

namespace Mafmax.InvestorService.Model.Specifications.Assets;

/// <summary>
/// Class filter for <see cref="AssetEntity"/>
/// </summary>
public class InClassSpecification : Specification<AssetEntity>
{
    private readonly string _className;
    private readonly StringComparison _searchType;

    /// <inheritdoc />
    public InClassSpecification(string className, StringComparison searchType)
    {
        _className = className;
        _searchType = searchType;
    }

    /// <inheritdoc />
    public override Expression<Func<AssetEntity, bool>> ToExpression() =>
        x => x.Class.Equals(_className, _searchType);
}
