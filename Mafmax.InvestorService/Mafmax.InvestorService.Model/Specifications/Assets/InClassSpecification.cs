using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqSpecs.Core;
using Mafmax.InvestorService.Model.Entities.Assets;

namespace Mafmax.InvestorService.Model.Specifications.Assets;

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
