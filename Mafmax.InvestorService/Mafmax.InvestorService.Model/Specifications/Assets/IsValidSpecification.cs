using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqSpecs.Core;
using Mafmax.InvestorService.Model.Entities.Assets;

namespace Mafmax.InvestorService.Model.Specifications.Assets;

public class IsValidSpecification : Specification<AssetEntity>
{
    public override Expression<Func<AssetEntity, bool>> ToExpression() =>
        x => x.Circulation.End.HasValue == false;
}
