using System;
using System.Linq.Expressions;
using LinqSpecs.Core;
using Mafmax.InvestorService.Model.Entities.Assets;

namespace Mafmax.InvestorService.Model.Specifications.Assets;

/// <summary>
/// Contains search string in Name of <see cref="AssetEntity"/>
/// </summary>
public class SearchByNameSpecification : Specification<AssetEntity>
{
    private readonly string _searchString;
    private readonly StringComparison _searchType;

    /// <inheritdoc />
    public SearchByNameSpecification(string searchString,StringComparison searchType)
    {
        _searchString = searchString;
        _searchType = searchType;
    }

    /// <inheritdoc />
    public override Expression<Func<AssetEntity, bool>> ToExpression() =>
        x => x.Name.Contains(_searchString,_searchType);
}
