using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafmax.InvestorService.Services.Tests.Queries.Base
{
    public abstract class InvestorServiceQueriesHandlerTestsBase<THandler>
    {
        protected THandler Handler=>GetHandler();

        protected abstract THandler GetHandler();
    }
}
