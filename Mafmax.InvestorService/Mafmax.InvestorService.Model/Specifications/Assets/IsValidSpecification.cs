using System;
using System.Linq.Expressions;
using LinqSpecs.Core;
using Mafmax.InvestorService.Model.Entities.Assets;

namespace Mafmax.InvestorService.Model.Specifications.Assets;

/// <summary>
/// Valid filter for <see cref="AssetEntity"/>
/// </summary>
public class IsValidSpecification : Specification<AssetEntity>
{
    /// <inheritdoc />
    public override Expression<Func<AssetEntity, bool>> ToExpression() =>
        x => x.Circulation.End.HasValue == false;
}
