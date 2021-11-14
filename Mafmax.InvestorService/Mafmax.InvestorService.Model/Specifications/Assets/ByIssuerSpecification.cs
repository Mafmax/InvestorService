using System;
using System.Linq.Expressions;
using LinqSpecs.Core;
using Mafmax.InvestorService.Model.Entities.Assets;

namespace Mafmax.InvestorService.Model.Specifications.Assets;

/// <summary>
/// Issuer filter for <see cref="AssetEntity"/>
/// </summary>
public class ByIssuerSpecification : Specification<AssetEntity>
{
    private readonly int _issuerId;

    /// <inheritdoc />
    public ByIssuerSpecification(int issuerId)
    {
        _issuerId = issuerId;
    }

    /// <inheritdoc />
    public override Expression<Func<AssetEntity, bool>> ToExpression() =>
        x => x.Issuer.Id.Equals(_issuerId);
}
